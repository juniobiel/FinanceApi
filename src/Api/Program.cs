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

            var IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            var connectionString = IsDevelopment ?
                builder.Configuration.GetConnectionString("DefaultConnection") :
                GetHerokuConnectionString();

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

        private static string GetHerokuConnectionString()
        {
            string connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(connectionUrl);

            string db = databaseUri.LocalPath.Trim('/');
            string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            return  $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};" +
                    $"Port={databaseUri.Port};Database={db};Pooling=true;" +
                    $"SSL Mode=Require;Trust Server Certificate=True;";
        }
    }

}