using System.Diagnostics.CodeAnalysis;

namespace Api.Extensions.User.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }
}
