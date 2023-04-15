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
using Microsoft.Extensions.Localization;
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

namespace Votp
{
    public class Program
    {
        public static void InitializeDB(VotpDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            {
                var r = Randomizer.Instance;
                var users = Enumerable.Range(0, 5)
                    .Select(i => new User()
                    {
                        Login = r.NextWord(5),
                        Tokens =
                        Enumerable.Range(1, r.Next(1, 3))
                        .Select(j => i == 0 ? new TimeToken() { Value = r.NextAlphaNum(3), RegistrationTime = DateTime.Now, Prefix = "1111" } :
                        new TotpToken() { Value = r.NextAlphaNum(3), RegistrationTime = DateTime.Now, Key = Convert.FromBase64String("VGhlIHF1aWNrIGJyb3duIGZveCA=") } as Token).ToList()
                    }
                    ).ToList();
                db.Users!.AddRange(users);
                db.Resolvers.Add(new ResolverInfo() { ResolverName = "Database" });
                //db.Resolvers.Add(new LdapUserResolverInfo() { ResolverName = "Ldap", Server = "localhost", Port = 10389, ConnectionLogin = "cn=admin,dc=example,dc=org", ConnectionPassword = "admin" }) ;

                db.SaveChanges();
            } 
        }

        public class PlaceholdLocalizator : IViewLocalizer
        {
            public LocalizedHtmlString this[string name] => new LocalizedHtmlString(name, name);

            public LocalizedHtmlString this[string name, params object[] arguments] => this[name];

            public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            {
                throw new NotImplementedException();
            }

            public LocalizedString GetString(string name)
            {
                return new LocalizedString(name, name);
            }

            public LocalizedString GetString(string name, params object[] arguments)
            {
                return GetString(name);
            }
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IDBLibService dbLibService = new RegistratorService();


            // Add services to the container.
            string? s = 
                Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? 
                builder.Configuration.GetConnectionString("Default");
            
            builder.Services.AddDbContext<IVotpDbContext, VotpDbContext>(o => o.UseSqlServer(s));


            builder.Services.AddSingleton<IResolverFactoryContainerService<User>>(p =>
                new UserResolverFactoryContainerService()
                .RegisterDatabaseUserResolver(dbLibService, p)
                .RegisterLdapUserResolver(dbLibService)
                );

            builder.Services.AddTransient<ITokenService, DBTokenService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ITokenCheckerService, TokenCheckerService>();
            builder.Services.AddTransient<IByteGeneratorService, ByteGeneratorService>();


            builder.Services.AddTransient<IViewLocalizer, PlaceholdLocalizator>();


            builder.Services.AddTransient<IUserResolverService, UserResolverService>();

            

            List<Assembly> assemblies = new List<Assembly>()
            {
                typeof(SystemTimeTokenController).Assembly
            };
            List<Type> mapperTypes = new List<Type>()
            {
                typeof(AutoMapperProfile)
            };
            

            builder.Services.AddTimedToken(assemblies, mapperTypes, dbLibService);

            builder.Services.AddSingleton<IDBLibService>(dbLibService);
            builder.Services.AddAutoMapper(mapperTypes.ToArray());

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
                using (var db = app.Services.CreateScope().ServiceProvider.GetRequiredService<IVotpDbContext>() as VotpDbContext)
                {
                    try
                    {
                        if (db != null)
                        {
                            InitializeDB(db);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
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