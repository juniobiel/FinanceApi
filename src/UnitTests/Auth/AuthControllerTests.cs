using Api.Controllers;
using Moq.AutoMock;

namespace UnitTests.Auth
{
    public class AuthControllerTests
    {
        private readonly AuthController _authService;
        private readonly AutoMocker mocker;

        public AuthControllerTests()
        {
            mocker = new AutoMocker();
            _authService = mocker.CreateInstance<AuthController>();
        }

        [Fact(DisplayName = "Criar conta e retornar sucesso")]
        [Trait("create-account", "sucess with account")]
        public void ReturnActionResult_StatusCode200_WithCreatedAccount()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
