using api_technical_tuya.Application.Interfaces;
using api_technical_tuya.Application.UseCases.Customer.DeleteCustomer;
using api_technical_tuya.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.WebApi.Test.Customers
{
    public class DeleteCustomerHandlerTests
    {
        [Fact]
        public async Task HandleAsync_ShouldDeleteCustomer_WhenExists()
        {
            var repoMock = new Mock<ICustomerRepository>();
            var uowMock = new Mock<IUnitOfWork>();

            var handler = new DeleteCustomerHandler(repoMock.Object, uowMock.Object);
            var cmd = new DeleteCustomerCommand(Guid.NewGuid());

            await handler.HandleAsync(cmd);

            repoMock.Verify(r => r.DeleteAsync(cmd.Id, default), Times.Once);
            uowMock.Verify(u => u.SaveChangesAsync(default), Times.Once);
        }
    }
}
