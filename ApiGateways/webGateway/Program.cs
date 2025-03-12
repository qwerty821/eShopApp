
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace webGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

            builder.Services.AddOcelot(builder.Configuration);
            var app = builder.Build();

            builder.Services.AddLogging(builder => builder.AddConsole());

		 
			//app.Map("/swagger/v1/swagger.json", b =>
			//{
			//    b.Run(async x =>
			//    {
			//        var json = File.ReadAllText("swagger.json");
			//        await x.Response.WriteAsync(json);
			//    });
			//});
			//app.UseSwaggerUI(c =>
			//{
			//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ocelot");
			//});

			//app.UseAuthorization();

			//app.UseHttpsRedirection();
			//if (app.Environment.IsDevelopment())
			//{
			//	app.UseSwagger();
			//	app.UseSwaggerUI();
			//}

			app.UseOcelot().Wait();

            app.Run();
        }
    }
}
