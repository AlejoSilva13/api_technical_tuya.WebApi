using api_technical_tuya.Application.Dtos;
using api_technical_tuya.Application.Interfaces;
using api_technical_tuya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.UseCases.Customer.UpdateCustomer
{
    public sealed class UpdateCustomerHandler
    {
        private readonly ICustomerRepository _repo;
        private readonly IUnitOfWork _uow;

        public UpdateCustomerHandler(ICustomerRepository repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

        public async Task<CustomerDto> HandleAsync(UpdateCustomerCommand cmd, CancellationToken ct = default)
        {
            var customer = await _repo.GetByIdAsync(cmd.Id, ct) ?? throw new InvalidOperationException("Customer not found");
            customer.Update(cmd.Name, cmd.Email);
            await _repo.UpdateAsync(customer, ct);
            await _uow.SaveChangesAsync(ct);

            return new CustomerDto(customer.Id, customer.Name, customer.Email, customer.CreatedAtUtc);
        }
    }
}
