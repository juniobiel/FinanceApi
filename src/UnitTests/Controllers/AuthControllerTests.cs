using Api.Configs.JWT;
using Api.Controllers;
using Api.Extensions.User.ViewModels;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace UnitTests.Controllers
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
        [Trait("create-account", "Status 200")]
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
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((IdentityUser) null));
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

        [Fact(DisplayName = "Criar conta com Model Incorreto")]
        [Trait("create-account", "Status 400")]
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

        [Fact(DisplayName = "Criar conta com Email já cadastrado")]
        [Trait("create-account", "Status 400")]
        public async Task CreateAccount_WhenEmailIsRegistered_ReturnStatusCode400()
        {
            //Arrange
            var newUser = new RegisterUserViewModel
            {
                Email = "teste@teste.com",
                Password = "teste",
                ConfirmPassword = "teste"
            };
            var userRegistered = new IdentityUser
            {
                Email = "teste@teste.com"
            };
            _mocker.GetMock<UserManager<IdentityUser>>()
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(userRegistered));

            //Act
            var result = await _authService.CreateAccount(newUser);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, result.StatusCode.Value);
            Assert.IsType<string>(result.Value);
        }

        [Fact(DisplayName = "Criar conta mockando error para o CreateAsync")]
        [Trait("create-account", "Status 400")]
        public async Task CreateAccount_WithResultErrorInCreateAsync_ReturnStatusCode400()
        {
            //Arrange
            var newUser = new RegisterUserViewModel
            {
                Email = "teste@teste.com",
                Password = "teste123456",
                ConfirmPassword = "teste123456"
            };
            var error = new IdentityError()
            {
                Description = "error",
                Code = "teste"
            };
            _mocker.GetMock<UserManager<IdentityUser>>()
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((IdentityUser)null));
            _mocker.GetMock<UserManager<IdentityUser>>()
                .Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(error));

            //Act
            var result = await _authService.CreateAccount(newUser);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, result.StatusCode.Value);
            Assert.IsType<List<string>>(result.Value);
        }

        [Fact(DisplayName = "Criar conta mockando error para o AddToRoleAsync")]
        [Trait("create-account", "Status 400")]
        public async Task CreateAccount_WithResultErrorInAddToRoleAsync_ReturnStatusCode400()
        {
            //Arrange
            var newUser = new RegisterUserViewModel
            {
                Email = "teste@teste.com",
                Password = "teste123456",
                ConfirmPassword = "teste123456"
            };
            var error = new IdentityError()
            {
                Description = "error",
                Code = "teste"
            };
            _mocker.GetMock<UserManager<IdentityUser>>()
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((IdentityUser)null));
            _mocker.GetMock<UserManager<IdentityUser>>()
                .Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mocker.GetMock<UserManager<IdentityUser>>()
                .Setup(x => x.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(error));

            //Act
            var result = await _authService.CreateAccount(newUser);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, result.StatusCode.Value);
            Assert.IsType<string>(result.Value);
        }

        [Fact(DisplayName = "Logar conta e retornar token")]
        [Trait("sign-in", "Status 200")]
        public async Task Login_WhenCalled_ReturnStatusCode200WithJWT()
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

        [Fact(DisplayName = "Logar conta mockando IsLockedOut para o PasswordSignInAsync ")]
        [Trait("sign-in", "Status 423")]
        public async Task Login_WithResultIsLockedOutInPasswordSignInAsync_ReturnStatusCode423()
        {
            //Arrange
            var loginUser = new LoginUserViewModel
            {
                Email = "teste@teste.com",
                Password = "teste123456"
            };
            _mocker.GetMock<SignInManager<IdentityUser>>()
                   .Setup(x => x.PasswordSignInAsync(It.IsAny<string>(),
                   It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                   .ReturnsAsync(SignInResult.LockedOut);

            //Act
            var result = await _authService.Login(loginUser);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(423, result.StatusCode.Value);
            Assert.IsType<string>(result.Value);
        }

        [Fact(DisplayName = "Logar conta mockando Error para o PasswordSignInAsync ")]
        [Trait("sign-in", "Status 400")]
        public async Task Login_WithResultErrorInPasswordSignInAsync_ReturnStatusCode400()
        {
            //Arrange
            var loginUser = new LoginUserViewModel
            {
                Email = "teste@teste.com",
                Password = "teste123456"
            };
            _mocker.GetMock<SignInManager<IdentityUser>>()
                   .Setup(x => x.PasswordSignInAsync(It.IsAny<string>(),
                   It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                   .ReturnsAsync(SignInResult.Failed);

            //Act
            var result = await _authService.Login(loginUser);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, result.StatusCode.Value);
            Assert.IsType<string>(result.Value);
        }

        [Fact(DisplayName = "Deslogar")]
        [Trait("sign-out", "Status 200")]
        public async Task Logout_WhenCalled_ReturnStatusCode200()
        {
            //Act
            var result = await _authService.Logout();

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<string>(result.Value);

        }
    }
}
