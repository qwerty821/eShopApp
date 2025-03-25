
using Identity.API.Contexts;
using Identity.API.Models;
using Identity.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
          
            var builder = WebApplication.CreateBuilder(args);
        
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Preserve property names as defined in the C# models (disable camelCase naming)
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });
         
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddDbContext<IdentityDBContext >(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("EFCoreDBConnection")));

            builder.Services.AddDbContext<IdentityDBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PSDBConnection")));

            //options.UseNpgsql(@"Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase"));
            //options.UseSqlServer(builder.Configuration.GetConnectionString("EFCoreDBConnection")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            builder.Services.AddHostedService<KeyRotationService>();
            
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                 
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,  
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],  
                    ValidateAudience = false,  
                    ValidateLifetime = true,  
                    ValidateIssuerSigningKey = true,  
                    IssuerSigningKeyResolver =   (token, securityToken, kid, parameters) =>
                    {
                        Console.WriteLine($"Received Token: {token}");
                        Console.WriteLine($"Token Issuer: {securityToken.Issuer}");
                        Console.WriteLine($"Key ID: {kid}");
                        Console.WriteLine($"Validate Lifetime: {parameters.ValidateLifetime}");

                        // Initialize an HttpClient instance for fetching the JWKS
                        //var httpClient = new HttpClient();

                        // Synchronously fetch the JWKS (JSON Web Key Set) from the specified URL
                        //string urlToFetch = $"{builder.Configuration["Jwt:Issuer"]}/.well-known/jwks.json";
                        //Console.WriteLine($"url to fetch = {urlToFetch}");
                        //var jwks = httpClient.GetStringAsync(urlToFetch)
                        // .GetAwaiter().GetResult();  // Blocking here to synchronously wait

                    var jwks = @"
                                {
                                  ""keys"": [
                                    {
                                      ""kty"": ""RSA"",
                                      ""use"": ""sig"",
                                      ""kid"": ""1beae5c4-7720-49bf-b983-ccb7823e91f1"",
                                      ""alg"": ""RS256"",
                                      ""n"": ""tdgCNfnIqqxIrhHPGluvv2UMZPoieWPQLjMq7Sa8og0EQM-zQUDW1fVfpGRXWzcmPpaZTmr0-tKha5phjuqyULqLBGF2rIpmIbgmQZcVvc9rVuOeBqwOV-Zhns5ZkXH2OWlHUzJ7hnBNqgeSCnVfNtl9_61affuiv3NH2Ko-K2qWJiDV_AkiypNt0Tei1N-IH_0rD_bnVAzPJQrQ1uYS30-9gRbivo9Wmn8hRTDvrJi3I20qA2WsYxEFGIRxUmDCFPu3L8NFH7a2osyiXvisNDxLot74BmkWcrbNS7GfRwkQtCrith8v9UJee-yrfLjb19z_l73q54xT7AYu5tmb2Q"",
                                      ""e"": ""AQAB""
                                    }
                                  ]
                                }";
                        Console.WriteLine("jwks = " + jwks); 
                        var keys = new JsonWebKeySet(jwks);

                        return keys.Keys;
                    }
                };
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();  
                app.UseSwaggerUI();  
            }

         

            app.UseHttpsRedirection();
            ///
            app.UseCors("AllowAllOrigins");
            /// 
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
