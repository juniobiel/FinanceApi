using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Api.Configs
{
    [ExcludeFromCodeCoverage]
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig( this IServiceCollection services )
        {
            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                options.AddPolicy("Production",
                builder => builder
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .WithOrigins("https://rococo-panda-df1aaf.netlify.app")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowCredentials());

            });

            return services;
        }
    }
}
