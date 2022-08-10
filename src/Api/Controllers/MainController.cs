using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IUser AppUser;

        protected Guid UserId { get; set; }
        protected bool UserAuthenticated { get; set; }

        public MainController(IUser appUser)
        {
            AppUser = appUser;

            if(AppUser.IsAuthenticated())
            {
                UserId = AppUser.GetUserId();
                UserAuthenticated = true;
            }
        }

    }
}
