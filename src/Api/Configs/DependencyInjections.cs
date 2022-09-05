﻿using Api.Configs.Swagger;
using Api.Extensions.User;
using Business.Interfaces;
using Business.Services.AlphaVantage;
using Business.Services.AssetService;
using Business.Services.ServiceKey;
using Data.Repositories;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using UnitTests.Services;

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

            services.AddScoped<IServiceKey, ServiceKey>();

            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAlphaVantageService, AlphaVantageService>();
            services.AddScoped<IAssetRepository, AssetRepository>();

            return services;
        }
    }
}
