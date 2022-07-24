using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Phone_book.Auth.Common;
using Phone_book.Data;
using Phone_book.Data.Models;
using Phone_book.Data.Repository;

namespace Phone_book;

public class Startup
{
    private readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        //var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

        //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer(options =>
        //    {
        //        options.RequireHttpsMetadata = false;
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidIssuer = authOptions.Issuer,

        //            ValidateAudience = true,
        //            ValidAudience = authOptions.Audience,

        //            ValidateLifetime = true,

        //            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
        //            ValidateIssuerSigningKey = true
        //        };
        //    });

        //services.AddCors(options =>
        //{
        //    options.AddDefaultPolicy(
        //        builder =>
        //        {
        //            builder.AllowAnyOrigin()
        //                .AllowAnyMethod()
        //                .AllowAnyHeader();
        //        });
        //});

        services.AddMvc(options => options.EnableEndpointRouting = false);

        string connectionString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<PhoneBookContext>(builder =>
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Phone_book.Data"))
        );
        services.AddScoped<IRepository<Contact>, ContactRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseHttpsRedirection();

        app.UseStaticFiles();


        app.UseRouting();
        //app.UseCors();

        //app.UseAuthorization();
        //app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "Default",
                pattern: "{controller=home}/{action=index}"
            );

            endpoints.MapControllerRoute(
                name: "UserById",
                pattern: "Details/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Details",
                }
            );

            endpoints.MapControllerRoute(
                name: "Add",
                pattern: "Add",
                defaults: new
                {
                    controller = "Home",
                    action = "Add",
                }
            );

        });
    }
}
