using Microsoft.EntityFrameworkCore;
using Votp.DS.Database;
using Votp.Services.Contracts;
using Votp.Services.Realizations;

namespace Votp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string? s = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<IVotpDbContext, VotpDbContext>(o => o.UseSqlServer(s));


            builder.Services.AddTransient<ITokenService, DBTokenService>();
            builder.Services.AddTransient<ITokenCheckerService, TokenCheckerService>();

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
                pattern: "{controller=Admin}/{action=Index}/{id?}");

            app.Run();
        }
    }
}