
using Microsoft.EntityFrameworkCore;
using Ordering.API.Contexts;

namespace Ordering.API
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
            builder.Services.AddCors(options =>
            {
                //options.AddPolicy("AllowAllOrigins",
                //    builder => builder.AllowAnyOrigin()
                //                      .AllowAnyMethod()
                //                      .AllowAnyHeader());

                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy
                        .WithOrigins(
                            "http://localhost:3000",
                            "https://localhost:3000",
                            "https://frontend.com"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            builder.Services.AddDbContext<OrderDbContext>(options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("PSDBConnection")));

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
