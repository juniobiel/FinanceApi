using Api.Data;
using Api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Api.Configs
{
    [ExcludeFromCodeCoverage]
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration( this IServiceCollection services, string connectionString )
        {
            services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(connectionString));
            services.AddIdentity<IdentityUser, IdentityRole>()
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<ApiDbContext>()
              .AddDefaultTokenProviders()
              .AddErrorDescriber<IdentityMensagensPortugues>();

            return services;
        }
    }
}
