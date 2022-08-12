using Api.Configs.JWT;
using Api.Controllers;
using Api.Extensions.User;
using Api.Extensions.User.ViewModels;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace UnitTests.Auth
{
    public class AuthControllerTests
    {
        private readonly AuthController _authService;
        private readonly AutoMocker _mocker;
        private readonly SignInManager<IdentityUser> _signInMock;
        private readonly UserManager<IdentityUser> _userManagerMock;


        public AuthControllerTests()
        {
            _mocker = new AutoMocker();
            
            var appSettingsConfig = new AppSettings
            {
                Secret = "25c85773-86d9-4d2a-9fae-5e912d85531c",
                ExpirationTime = 5,
                Issuer = "FinanceApi",
                ValidAt = "https://localhost"
            };
            var appSettings = Options.Create(appSettingsConfig);

            //Faz o automocking utilizando uma instancia de mock ao invés do tipo original
            _signInMock = _mocker.WithSelfMock<SignInManager<IdentityUser>>();
            _userManagerMock = _mocker.WithSelfMock<UserManager<IdentityUser>>();

            _authService = new AuthController(new Mock<IUser>().Object,
                _signInMock,
                _userManagerMock,
                appSettings);
        }

        [Fact(DisplayName = "Criar conta e retornar sucesso")]
        [Trait("create-account", "sucess with account")]
        public async Task CreateAccount_WhenCalled_ReturnStatusCode200()
        {
            //Arrange
            var newUser = new RegisterUserViewModel
            {
                Email = "teste@teste.com",
                Password = "teste123456",
                ConfirmPassword = "teste123456"
            };

            _mocker.GetMock<UserManager<IdentityUser>>()
                .Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mocker.GetMock<UserManager<IdentityUser>>()
                .Setup(x => x.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            //Act
            var result = await _authService.CreateAccount(newUser);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, result.StatusCode.Value);
            Assert.IsType<RegisterUserViewModel>(result.Value);
        }

        [Fact(DisplayName = "Criar conta com Model Incorreto e retornar BadRequest")]
        [Trait("create-account", "error with viewmodel")]
        public async Task CreateAccount_WhenModelStateIsNotValid_ReturnStatusCode400()
        {
            //Arrange
            var newUser = new RegisterUserViewModel
            {
                Email = "teste",
                Password = "teste",
                ConfirmPassword = "teste"
            };
            _authService.ModelState.AddModelError("error", "ModelState is not valid");

            //Act
            var result = await _authService.CreateAccount(newUser);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, result.StatusCode.Value);
            Assert.IsType<SerializableError>(result.Value);
        }

        [Fact(DisplayName = "Logar conta e retornar token")]
        [Trait("sign-in", "success with token")]
        public async Task SignIn_WhenCalled_ReturnStatusCode200WithJWT()
        {
            //Arrange
            var loginUser = new LoginUserViewModel
            {
                Email = "teste@teste.com",
                Password = "teste123456"
            };
            var user = new IdentityUser
            {
                Email = "teste@teste.com",
                Id = Guid.NewGuid().ToString(),
            };
            var claims = new List<Claim>();
            var userRoles = new List<string>
            {
                "teste"
            };

            _mocker.GetMock<SignInManager<IdentityUser>>()
                    .Setup(x => x.PasswordSignInAsync(It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                    .ReturnsAsync(SignInResult.Success);
            _mocker.GetMock<UserManager<IdentityUser>>()
                    .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                    .ReturnsAsync(user);
            _mocker.GetMock<UserManager<IdentityUser>>()
                    .Setup(x => x.GetClaimsAsync(It.IsAny<IdentityUser>()))
                    .ReturnsAsync(claims);
            _mocker.GetMock<UserManager<IdentityUser>>()
                    .Setup(x => x.GetRolesAsync(It.IsAny<IdentityUser>()))
                    .ReturnsAsync(userRoles);

            //Act
            var result = await _authService.Login(loginUser);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, result.StatusCode.Value);
            Assert.IsType<LoginResponseViewModel>(result.Value);
        }
    }
}
