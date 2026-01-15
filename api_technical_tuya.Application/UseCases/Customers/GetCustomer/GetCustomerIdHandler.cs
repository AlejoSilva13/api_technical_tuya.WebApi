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
        private readonly ICustomerRepository _repository;
        public GetCustomerIdHandler(ICustomerRepository repo) => _repository = repo;

        public async Task<CustomerDto> HandleGetIdAsync(GetCustomerByIdQuery query, CancellationToken cancellationToken = default)
        {
            var list = await _repository.GetByIdAsync(query.Id, cancellationToken) ?? throw new InvalidOperationException("Customer not found");
            return new CustomerDto(list.Id, list.Name, list.Email, list.CreatedAtUtc);
        }
    }
}
