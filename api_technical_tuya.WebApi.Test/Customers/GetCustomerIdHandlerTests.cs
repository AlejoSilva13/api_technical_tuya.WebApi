using api_technical_tuya.Application.UseCases.Customer.GetCustomer;
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
    public class GetCustomerIdHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnCustomer_WhenExists()
        {
            var customer = new Customer(Guid.NewGuid(), "Diego", "diego@example.com", DateTime.UtcNow);

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(customer.Id, default)).ReturnsAsync(customer);

            var handler = new GetCustomerIdHandler(repoMock.Object);

            var result = await handler.HandleAsync(new GetCustomerByIdQuery(customer.Id));

            Assert.Equal(customer.Id, result.Id);
        }

        [Fact]
        public async Task HandleAsync_ShouldThrow_WhenCustomerNotFound()
        {
            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), default)).ReturnsAsync((Customer?)null);

            var handler = new GetCustomerIdHandler(repoMock.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                handler.HandleAsync(new GetCustomerByIdQuery(Guid.NewGuid())));
        }
    }
}
