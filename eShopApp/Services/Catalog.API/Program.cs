
using Catalog.API.Abstractions;
using Catalog.API.Repositories;
using Catalog.API.Services;
using Couchbase;
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

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var config = builder.Configuration;
            var connStr = config["Couchbase:ConnectionString"];
            var user = config["Couchbase:Username"];
            var pass = config["Couchbase:Password"];
            var bucket = config["Couchbase:BucketName"];

            builder.Services.AddSingleton(async _ =>
            {
                var cluster = await Cluster.ConnectAsync(connStr, user, pass);
                return cluster;
            });

            builder.Services.AddSingleton(async _ =>
            {
                var cluster = await Cluster.ConnectAsync(connStr, user, pass);
                var bucketRef = await cluster.BucketAsync(bucket);
                return bucketRef.DefaultCollection();
            });




            builder.Services.AddScoped<ICatalogItemRepository, CatalogItemRepository>();
            builder.Services.AddScoped<ICatalogService, CatalogService>();
            builder.Services.AddLogging();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog API V1");
                    //c.RoutePrefix = string.Empty;  
                });
            //}

            //app.UseHttpsRedirection();

            //app.UseAuthorization();
            app.UseCors("AllowAllOrigins");
            app.MapControllers();


            app.Run();
        }
    }
}
