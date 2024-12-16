using Microsoft.EntityFrameworkCore;
using Viber.Models;
using Viber.Services.Interfaces;
using Viber.Services.Services;

namespace Viber
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Fundet p� https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-strings?tabs=dotnet-core-cli
            var conString = builder.Configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection'" +
             " not found.");
            builder.Services.AddDbContext<finsby_dk_db_viberContext>(options =>
                options.UseSqlServer(conString));

            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<IMoodboardService, MoodboardService>();
            builder.Services.AddScoped<IPrimaryTagService, PrimaryTagService>();
            builder.Services.AddScoped<ISubTagService, SubTagService>();
            builder.Services.AddScoped<IContentContainerService, ContentContainerService>();
            
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole(); 
            builder.Logging.AddDebug();
            
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.UseSession();

            app.Run();
        }
    }
}
