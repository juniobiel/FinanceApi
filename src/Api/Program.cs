using Api.Configs.Swagger;
using Api.Configs.JWT;
using Api.Configs;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Diagnostics.CodeAnalysis;

namespace Api
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static void Main( string[] args )
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddApiConfig();
            builder.Services.AddDependenciesInjections();
            builder.Services.AddSwaggerConfig();
            builder.Services.AddIdentityConfiguration(connectionString);
            builder.Services.AddJWTConfiguration(builder.Configuration);

            var app = builder.Build();

            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseSwaggerConfig(apiVersionDescriptionProvider);

            app.Run();
        }
    }

}