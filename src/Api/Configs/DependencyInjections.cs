using Api.Configs.Swagger;
using Api.Extensions.User;
using Business.Interfaces;
using Business.Services.AlphaVantage;
using Business.Services.AssetService;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace Api.Configs
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjections
    {
        public static IServiceCollection AddDependenciesInjections(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAlphaVantageService, AlphaVantageService>();
            services.AddScoped<IConfiguration>();

            return services;
        }
    }
}
