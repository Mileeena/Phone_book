using Microsoft.EntityFrameworkCore;
using Phone_book.Data;
using Phone_book.Data.Models;
using Phone_book.Data.Repository;

namespace Phone_book.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<PhoneBookContext>(builder =>
                builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("Phone_book.Data"))
            );
            builder.Services.AddScoped<IRepository<Contact>, ContactRepository>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}