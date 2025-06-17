
using Cart.API.Abstractions;
using Cart.API.Contexts;
using Cart.API.Repositories;
using Cart.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Cart.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
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

            builder.Services.AddDbContext<CartDBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PSDBConnection")));

            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigins");
            
            app.UseAuthorization();

            app.MapControllers();

            app.UseResponseCaching();

            app.Run();
        }
    }
}
