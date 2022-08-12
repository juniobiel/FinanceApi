using Api.Controllers;
using Api.Extensions.User.ViewModels;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;

namespace UnitTests.Auth
{
    public class AuthControllerTests
    {
        private readonly AuthController _authService;
        private readonly AutoMocker _mocker;

        public AuthControllerTests()
        {
            _mocker = new AutoMocker();
            _authService = _mocker.CreateInstance<AuthController>();
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
    }
}
