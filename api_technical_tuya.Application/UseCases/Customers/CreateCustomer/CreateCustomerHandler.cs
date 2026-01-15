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
        private readonly ICustomerRepository _repo;
        private readonly IDateTimeProvider _clock;
        private readonly IUnitOfWork _uow;

        public CreateCustomerHandler(ICustomerRepository repo, IDateTimeProvider clock, IUnitOfWork uow)
        {
            _repo = repo; 
            _clock = clock;
            _uow = uow;
        }

        public async Task<CustomerDto> HandleCreateAsync(CreateCustomerCommand cmd, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(cmd.Name)) throw new ArgumentException("Name is required");
            if (string.IsNullOrWhiteSpace(cmd.Email)) throw new ArgumentException("Email is required");

            var customer = new DomainCustomer(Guid.Empty, cmd.Name, cmd.Email, _clock.UtcNow);
            await _repo.AddAsync(customer, ct);
            await _uow.SaveChangesAsync(ct);

            return new CustomerDto(customer.Id, customer.Name, customer.Email, customer.CreatedAtUtc);
        }
    }
}
