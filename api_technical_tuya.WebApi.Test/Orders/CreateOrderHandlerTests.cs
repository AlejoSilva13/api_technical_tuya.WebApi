using api_technical_tuya.Application.Interfaces;
using api_technical_tuya.Application.UseCases.Orders.CreateOrder;
using api_technical_tuya.Domain.Entities;
using api_technical_tuya.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.WebApi.Test.Orders
{
    public class CreateOrderHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldCreateOrder_WhenCustomerExists()
        {
            var customerId = Guid.NewGuid();
            var customer = new Customer(customerId, "Diego", "diego@example.com", DateTime.UtcNow);

            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(r => r.GetByIdAsync(customerId, default)).ReturnsAsync(customer);

            var orderRepoMock = new Mock<IOrderRepository>();
            var uowMock = new Mock<IUnitOfWork>();
            var clockMock = new Mock<IDateTimeProvider>();
            clockMock.Setup(c => c.UtcNow).Returns(DateTime.UtcNow);

            var handler = new CreateOrderHandler(customerRepoMock.Object, orderRepoMock.Object, clockMock.Object, uowMock.Object);
            var cmd = new CreateOrderCommand(customerId, 100m);

            var result = await handler.HandleAsync(cmd);

            Assert.Equal("Created", result.Status);
            orderRepoMock.Verify(r => r.AddAsync(It.IsAny<Order>(), default), Times.Once);
            uowMock.Verify(u => u.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ShouldThrow_WhenCustomerDoesNotExist()
        {
            var customerRepoMock = new Mock<ICustomerRepository>();
            customerRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), default)).ReturnsAsync((Customer?)null);

            var handler = new CreateOrderHandler(customerRepoMock.Object, new Mock<IOrderRepository>().Object, new Mock<IDateTimeProvider>().Object, new Mock<IUnitOfWork>().Object);
            var cmd = new CreateOrderCommand(Guid.NewGuid(), 100m);

            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleAsync(cmd));
        }

        [Fact]
        public async Task HandleAsync_ShouldThrow_WhenTotalIsNegative()
        {
            var handler = new CreateOrderHandler(new Mock<ICustomerRepository>().Object, new Mock<IOrderRepository>().Object, new Mock<IDateTimeProvider>().Object, new Mock<IUnitOfWork>().Object);
            var cmd = new CreateOrderCommand(Guid.NewGuid(), -10m);

            await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleAsync(cmd));
        }
    }
}
