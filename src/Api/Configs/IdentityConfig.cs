﻿using Api.Data;
using Api.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Configs
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<IdentityUser, IdentityRole>()
              .AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<ApiDbContext>()
              .AddDefaultTokenProviders()
              .AddErrorDescriber<IdentityMensagensPortugues>();

            return services;
        }
    }
}
