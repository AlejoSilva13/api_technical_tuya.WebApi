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
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> HandleUpdateAsync(UpdateCustomerCommand command, CancellationToken cancellationToken = default)
        {
            var customer = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new InvalidOperationException("Customer not found");
            customer.Update(command.Name, command.Email);
            await _repository.UpdateAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CustomerDto(customer.Id, customer.Name, customer.Email, customer.CreatedAtUtc);
        }
    }
}
