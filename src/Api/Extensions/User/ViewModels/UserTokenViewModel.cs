using System.Diagnostics.CodeAnalysis;

namespace Api.Extensions.User.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}
