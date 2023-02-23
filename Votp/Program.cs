using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Votp.Contracts.Services;
using Votp.Contracts.Services.UserResolver;
using Votp.DS.Database;
using Votp.DS.Database.Entities;
using Votp.Services.Realizations;
using Votp.Services.Realizations.DatabaseUserResolver;
using Votp.Services.Realizations.UserResolver;

namespace Votp
{
    public class Program
    {
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

            // Add services to the container.
            string? s = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<IVotpDbContext, VotpDbContext>(o => o.UseSqlServer(s));

            builder.Services.AddSingleton<IResolverFactoryContainerService<User>>(p =>
                new UserResolverFactoryContainerService().RegisterDatabaseUserResolver(p)
                );

            builder.Services.AddTransient<ITokenService, DBTokenService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ITokenCheckerService, TokenCheckerService>();


            builder.Services.AddTransient<IViewLocalizer, PlaceholdLocalizator>();





            builder.Services.AddTransient<IUserResolverService, UserResolverService>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddControllersWithViews();
            

            var app = builder.Build();

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