using api_technical_tuya.Application.Dtos;
using api_technical_tuya.Application.Interfaces;
using api_technical_tuya.Application.UseCases.Customer.CreateCustomer;
using DomainCustomer = api_technical_tuya.Domain.Entities.Customer;
using api_technical_tuya.Domain.Entities;
using api_technical_tuya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.UseCases.Customers.CreateCustomer
{
    public sealed class CreateCustomerHandler
    {
        private readonly ICustomerRepository _repository;
        private readonly IDateTimeProvider _clock;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerHandler(ICustomerRepository repository, IDateTimeProvider clock, IUnitOfWork unitOfWork)
        {
            _repository = repository; 
            _clock = clock;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> HandleCreateAsync(CreateCustomerCommand command, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(command.Name)) throw new ArgumentException("Name is required");
            if (string.IsNullOrWhiteSpace(command.Email)) throw new ArgumentException("Email is required");

            var customer = new DomainCustomer(Guid.Empty, command.Name, command.Email, _clock.UtcNow);
            await _repository.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CustomerDto(customer.Id, customer.Name, customer.Email, customer.CreatedAtUtc);
        }
    }
}
