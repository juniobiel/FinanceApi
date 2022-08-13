using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace Api.Configs.Swagger
{
    [ExcludeFromCodeCoverage]
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions( IApiVersionDescriptionProvider provider ) => this.provider = provider;

        public void Configure( SwaggerGenOptions options )
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion( ApiVersionDescription description )
        {
            var uri = "https://opensource.org/licenses/MIT";
            var info = new OpenApiInfo
            {
                Title = "Finance API",
                Version = description.ApiVersion.ToString(),
                Description = "API para controle e gestão de Finanças",
                Contact = new OpenApiContact() { Name = "Gabriel Júnio", Email = "gabrieljunio.fp@gmail.com" },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri(uri) }
            };

            if (description.IsDeprecated)
            {
                info.Description += "Esta versão está obsoleta";
            }

            return info;
        }
    }
}