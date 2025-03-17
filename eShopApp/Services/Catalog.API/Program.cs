
using Catalog.API.Abstractions;
using Catalog.API.Contexts;
using Catalog.API.Repositories;
using Catalog.API.Services;
using Microsoft.EntityFrameworkCore;

namespace eShopApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<CatalogDBContext>(options => options.UseSqlServer(connection));


            builder.Services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
            builder.Services.AddScoped<CatalogService>();
            builder.Services.AddLogging();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
