using api_technical_tuya.Application.Interfaces;
using api_technical_tuya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.UseCases.Customer.DeleteCustomer
{

    public sealed class DeleteCustomerHandler
    {
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task HandleDeleteAsync(DeleteCustomerCommand command, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(command.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }

}
