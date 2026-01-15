using api_technical_tuya.Application.Interfaces;
using api_technical_tuya.Domain.Entities;
using api_technical_tuya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.UseCases.Orders.CreateOrder
{

    public sealed class CreateOrderHandler
    {
        private readonly ICustomerRepository _customers;
        private readonly IOrderRepository _orders;
        private readonly IDateTimeProvider _clock;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderHandler(ICustomerRepository customers, IOrderRepository orders, IDateTimeProvider clock, IUnitOfWork uow)
        {
            _customers = customers; 
            _orders = orders; 
            _clock = clock;
            _unitOfWork = uow;
        }

        public async Task<CreateOrderResult> HandleCreateAsync(CreateOrderCommand command, CancellationToken cancellationToken = default)
        {
            if (command.CustomerId == Guid.Empty) throw new ArgumentException("CustomerId is required");
            if (command.Total < 0) throw new ArgumentException("Total cannot be negative");

            var customer = await _customers.GetByIdAsync(command.CustomerId, cancellationToken);
            if (customer is null) throw new InvalidOperationException("Customer does not exist");

            var order = new Order(command.CustomerId, command.Total, _clock.UtcNow);
            await _orders.AddAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id, order.Status);
        }
    }

}
