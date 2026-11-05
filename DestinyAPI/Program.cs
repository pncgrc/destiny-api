
using DestinyAPI.Data;
using DestinyAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace DestinyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            var useInMemory = false;

            if (useInMemory)
            {
                builder.Services.AddSingleton<IFactionRepository, InMemoryFactionRepository>();
                builder.Services.AddSingleton<IWeaponRepository, InMemoryWeaponRepository>();
                builder.Services.AddSingleton<ICharacterRepository, InMemoryCharacterRepository>();
            }
            else
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                builder.Services.AddDbContext<DestinyDbContext>(options =>
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

                builder.Services.AddScoped<IFactionRepository, EfFactionRepository>();
                builder.Services.AddScoped<ICharacterRepository, EfCharacterRepository>();
                builder.Services.AddScoped<IWeaponRepository, EfWeaponRepository>();
            }        

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
