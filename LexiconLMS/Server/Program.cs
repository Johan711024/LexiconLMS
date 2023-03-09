using LexiconLMS.Server.Data;
using LexiconLMS.Server.Extensions;
using LexiconLMS.Server.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using static System.Formats.Asn1.AsnWriter;
using LexiconLMS.Server.MyModels;
using Microsoft.Extensions.DependencyInjection;
using LexiconLMS.Server.Repositories;

namespace LexiconLMS.Server
{
    public /*static*/ class Program
    {
        public static async Task Main(string[] args)
        {
            //IServiceProvider serviceProvider = new();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            //builder.Services.AddAutoMapper(typeof(Program));

            //// Make sure we have the database
            //serviceProvider.GetService<ApplicationDbContext>().Database.EnsureCreated();
        

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>() //<-----
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //1. Register UnitOFWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddIdentityServer()
            //***********************************************************************//
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                {
                    options.IdentityResources["openid"].UserClaims.Add("role");
                    options.ApiResources.Single().UserClaims.Add("role");
                });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("role");
            //***********************************************************************//
            builder.Services.AddAuthentication()
                .AddIdentityServerJwt();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            //builder.MyModels.MockDataService();
            var app = builder.Build();

            await app.SeedDataAsync();

            



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();

            //await builder.Build().RunAsync();

        }
    }
}