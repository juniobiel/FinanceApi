﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext( DbContextOptions<ApiDbContext> options ) : base(options) { }

    }
}
