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
        public async Task ReturnActionResult_StatusCode200_WithCreatedAccount()
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
                .Returns(Task.FromResult(new IdentityResult()));

            //Act
            var result = await _authService.CreateAccount(newUser);

            //Assert
            Assert.IsType<ActionResult<OkResult>>(result);

        }
    }
}
