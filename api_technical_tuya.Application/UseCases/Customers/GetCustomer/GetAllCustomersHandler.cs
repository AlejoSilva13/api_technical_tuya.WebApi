using api_technical_tuya.Application.Dtos;
using api_technical_tuya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.UseCases.Customer.GetCustomer
{

    public sealed class GetAllCustomersHandler
    {
        private readonly ICustomerRepository _repository;
        public GetAllCustomersHandler(ICustomerRepository repo) => _repository = repo;

        public async Task<IReadOnlyList<CustomerDto>> HandleGetAllAsync(GetAllCustomersQuery getCustomer, CancellationToken cancellationToken = default)
        {
            var list = await _repository.GetAllAsync(cancellationToken);
            return list.Select(x => new CustomerDto(x.Id, x.Name, x.Email, x.CreatedAtUtc)).ToList();
        }
    }

}
