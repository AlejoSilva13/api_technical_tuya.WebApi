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
    public class GetAllCustomersHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldReturnCustomers()
        {
            var customers = new List<Customer>
        {
            new Customer(Guid.NewGuid(), "Diego", "diego@example.com", DateTime.UtcNow)
        };

            var repoMock = new Mock<ICustomerRepository>();
            repoMock.Setup(r => r.GetAllAsync(default)).ReturnsAsync(customers);

            var handler = new GetAllCustomersHandler(repoMock.Object);

            var result = await handler.HandleAsync(new GetAllCustomersQuery());

            Assert.Single(result);
            Assert.Equal("Diego", result[0].Name);
        }
    }
}
