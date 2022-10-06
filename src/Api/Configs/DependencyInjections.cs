using Api.Configs.Swagger;
using Api.Extensions.User;
using Business.Interfaces;
using Business.Services.AlphaVantage;
using Business.Services.AssetPriceService;
using Business.Services.ServiceKey;
using Business.Services.UserAssetService;
using Data.Repositories;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace Api.Configs
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjections
    {
        public static IServiceCollection AddDependenciesInjections( this IServiceCollection services )
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddScoped<IServiceKey, ServiceKey>();
            services.AddScoped<IAlphaVantageService, AlphaVantageService>();

            services.AddScoped<IAssetPriceService, AssetPriceService>();
            services.AddScoped<IAssetPriceRepository, AssetPriceRepository>();

            services.AddScoped<IUserAssetRepository, UserAssetRepository>();
            services.AddScoped<IUserAssetService, UserAssetService>();

            return services;
        }
    }
}
