using Api.Controllers;
using Api.Extensions.User.ViewModels;
using Business.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
    }
}
