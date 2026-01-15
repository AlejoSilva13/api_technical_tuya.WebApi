using api_technical_tuya.Application.Dtos;
using api_technical_tuya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.UseCases.Customer.GetCustomer
{
    public sealed class GetCustomerIdHandler
    {
        private readonly ICustomerRepository _repo;
        public GetCustomerIdHandler(ICustomerRepository repo) => _repo = repo;

        public async Task<CustomerDto> HandleGetIdAsync(GetCustomerByIdQuery query, CancellationToken ct = default)
        {
            var c = await _repo.GetByIdAsync(query.Id, ct) ?? throw new InvalidOperationException("Customer not found");
            return new CustomerDto(c.Id, c.Name, c.Email, c.CreatedAtUtc);
        }
    }
}
