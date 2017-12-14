using Lucy.Services.Dtos;
using Lucy.Services.Interfaces;
using Lucy.Tools;
using Moq;
using Xunit;

namespace Lucy.Test
{
    public class TestUpdatePassword
    {
        [Fact]
        public void UpdatePasswordOldPwdNotRight()
        {
            var model = new AccountModel.ModifyPwd { ConfirmPwd = "123456", NewPwd = "123456", OldPwd = "888888", UserID = 1 };
            var accountService = new Mock<IAccountSvc>();
            accountService
                .Setup(c => c.UpdatePassword(model))
                .Returns(ResultEx.Init());
            // Target object
            var presenter = new AccountPresenter(accountService.Object);
            // Act
            var result = presenter.UpdatePassword(model);
            // Assert
            Assert.NotNull(result);
            Assert.True(result.flag);
            Assert.Empty(result.msg);
        }
    }
}
