using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Votp.Contracts.Services;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Database;
using Votp.DS.Entities;
using Votp.Services;
using Votp.UserResolver.InnerDatabase;
using Votp.Services.UserResolver;
using Microsoft.AspNetCore.Components.Forms;
using Votp.Utils;
using System.Reflection;
using Votp.UserResolver.Ldap;
using Votp.Tokens.Time.Controllers;
using Votp.Contracts;
using Votp.Tokens.Time.Entities;
using Votp.Tokens.Time;
using Votp.Tokens.Totp.Entities;
using Votp.Tokens;

namespace Votp
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string? s = 
                Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? 
                builder.Configuration.GetConnectionString("Default");
            string? sUsers =
                Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                builder.Configuration.GetConnectionString("DefaultUsers");

            builder.Services.AddDbContext<IVotpDbContext, VotpDbContext>(o => o.UseSqlServer(s));
            builder.Services.AddDbContext<IInnerUsersDBContext, InnerUsersDBContext>(o => o.UseSqlServer(sUsers));

            builder.Services.AddSingleton<IResolverFactoryContainerService<User>>(p =>
                new UserResolverFactoryContainerService()
                .RegisterDatabaseUserResolver(p)
                .RegisterLdapUserResolver()
                );
            builder.Services.AddTransient<ITokenService, DBTokenService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ITokenCheckerService, TokenCheckerService>();
            builder.Services.AddTransient<IByteGeneratorService, ByteGeneratorService>();
            builder.Services.AddTransient<IUserResolverService, UserResolverService>();
            builder.Services.AddTransient<IViewLocalizer, PlaceholdLocalizator>();

            IDBLibService dbLibService = new RegistratorService();
            dbLibService.LibAssemblies.Add(typeof(SystemTimeTokenController).Assembly);
            builder.Services.AddSingleton<IDBLibService>(dbLibService);

            List<Type> mapperTypes = new List<Type>()
            {
                typeof(AutoMapperProfile),
                typeof(TokensMapperProfile)
            };
            builder.Services.AddAutoMapper(mapperTypes.ToArray());

            List<Assembly> assemblies = new List<Assembly>()
            {
                typeof(SystemTimeTokenController).Assembly,
                typeof(DatabaseUserResolverInfo).Assembly
            };

            var mvcBuilder = builder.Services.AddControllersWithViews();
            foreach (var assembly in assemblies)
            {
                mvcBuilder.AddApplicationPart(assembly);
            }
            mvcBuilder.AddRazorRuntimeCompilation(o =>
                {
                    foreach (var assembly in assemblies)
                    {
                        o.FileProviders.Add(new EmbeddedFileProvider(assembly));
                    }
                });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                //DebugInitializer.InitializeDatabases(app.Services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Tokens}/{action=Index}/{id?}");

            app.Run();
        }
    }
}