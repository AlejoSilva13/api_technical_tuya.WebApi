using Moq;
using api_technical_tuya.Domain.Interfaces;
using api_technical_tuya.Application.Interfaces;
using api_technical_tuya.Application.UseCases.Customers.CreateCustomer;
using api_technical_tuya.Application.UseCases.Customer.CreateCustomer;
using api_technical_tuya.Domain.Entities;

namespace api_technical_tuya.WebApi.Test.Customers
{

    public class CreateCustomerHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldCreateCustomer_WhenDataIsValid()
        {
            var repoMock = new Mock<ICustomerRepository>();
            var uowMock = new Mock<IUnitOfWork>();
            var clockMock = new Mock<IDateTimeProvider>();
            clockMock.Setup(c => c.UtcNow).Returns(DateTime.UtcNow);

            var handler = new CreateCustomerHandler(repoMock.Object, clockMock.Object, uowMock.Object);
            var cmd = new CreateCustomerCommand("Diego", "diego@example.com");

            var result = await handler.HandleCreateAsync(cmd);

            Assert.Equal(cmd.Name, result.Name);
            Assert.Equal(cmd.Email.ToLowerInvariant(), result.Email);
            repoMock.Verify(r => r.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
            uowMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ShouldThrow_WhenNameIsEmpty()
        {
            var handler = new CreateCustomerHandler(
                new Mock<ICustomerRepository>().Object,
                new Mock<IDateTimeProvider>().Object,
                new Mock<IUnitOfWork>().Object);

            var cmd = new CreateCustomerCommand("", "diego@example.com");

            await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleCreateAsync(cmd));
        }

        [Fact]
        public async Task HandleAsync_ShouldThrow_WhenEmailIsEmpty()
        {
            var handler = new CreateCustomerHandler(
                new Mock<ICustomerRepository>().Object,
                new Mock<IDateTimeProvider>().Object,
                new Mock<IUnitOfWork>().Object);

            var cmd = new CreateCustomerCommand("Diego", "");

            await Assert.ThrowsAsync<ArgumentException>(() => handler.HandleCreateAsync(cmd));
        }
    }


}
