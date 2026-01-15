using api_technical_tuya.Application.UseCases.Orders.GetOrder;
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
    public class GetOrderHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnOrder_WhenExists()
        {
            var order = new Order(Guid.NewGuid(), 150m, DateTime.UtcNow);

            var repoMock = new Mock<IOrderRepository>();
            repoMock.Setup(r => r.GetByIdAsync(order.Id, default)).ReturnsAsync(order);

            var handler = new GetOrderHandler(repoMock.Object);

            var result = await handler.HandleAsync(new GetOrderIdCommand(order.Id));

            Assert.Equal(order.Id, result.Id);
        }

        [Fact]
        public async Task HandleAsync_ShouldThrow_WhenOrderNotFound()
        {
            var repoMock = new Mock<IOrderRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), default)).ReturnsAsync((Order?)null);

            var handler = new GetOrderHandler(repoMock.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.HandleAsync(new GetOrderIdCommand(Guid.NewGuid())));
        }
    }
}
