using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Api.Data
{
    [ExcludeFromCodeCoverage]
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext( DbContextOptions<ApiDbContext> options ) : base(options) { }

    }
}
