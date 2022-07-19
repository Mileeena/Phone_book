using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Phone_book.Auth.Common;
using Phone_book.Data;
using Phone_book.Data.Models;
using Phone_book.Data.Repository;

namespace Phone_book.Api;

public class Startup
{
    ///обратиться к appsettings
    private readonly IConfiguration Configuration;

    ///env - все инофрмация о приложении
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    /// добавление сервисов
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,

                    ValidateLifetime = true,

                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        services.AddMvc(options => options.EnableEndpointRouting = false);

        string connectionString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<PhoneBookContext>(builder =>
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Phone_book.Data"))
        );
        services.AddScoped<IRepository<Contact>, ContactRepository>();

        //services.AddSwaggerGen(c =>
        //{
        //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //    c.IncludeXmlComments(xmlPath);
        //});
    }
    ///подключение контролеров, порядок важен
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //app.UseSwagger();

        //app.UseSwaggerUI(c =>
        //{
        //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        //});

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}