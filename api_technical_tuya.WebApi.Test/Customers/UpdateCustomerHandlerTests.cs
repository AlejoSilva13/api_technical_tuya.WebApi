using api_technical_tuya.Application.Interfaces;
using api_technical_tuya.Application.UseCases.Customer.UpdateCustomer;
using api_technical_tuya.Domain.Entities;
using api_technical_tuya.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.WebApi.Test.Customers
{
    public class UpdateCustomerHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldUpdateCustomer_WhenExists()
        {
            var customer = new Customer(Guid.NewGuid(), "OldName", "old@example.com", DateTime.UtcNow);

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(customer.Id, default)).ReturnsAsync(customer);

            var uowMock = new Mock<IUnitOfWork>();

            var handler = new UpdateCustomerHandler(repoMock.Object, uowMock.Object);
            var cmd = new UpdateCustomerCommand(customer.Id, "NewName", "new@example.com");

            var result = await handler.HandleAsync(cmd);

            Assert.Equal("NewName", result.Name);
            Assert.Equal("new@example.com", result.Email);
            repoMock.Verify(r => r.UpdateAsync(customer, default), Times.Once);
            uowMock.Verify(u => u.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ShouldThrow_WhenCustomerNotFound()
        {
            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), default)).ReturnsAsync((Customer?)null);

            var handler = new UpdateCustomerHandler(repoMock.Object, new Mock<IUnitOfWork>().Object);
            var cmd = new UpdateCustomerCommand(Guid.NewGuid(), "Name", "email@example.com");

            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(cmd));
        }
    }
}
