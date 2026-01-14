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
        private readonly ICustomerRepository _repo;
        private readonly IUnitOfWork _uow;

        public DeleteCustomerHandler(ICustomerRepository repo, IUnitOfWork uow)
        {
            _repo = repo; 
            _uow = uow;
        }

        public async Task HandleAsync(DeleteCustomerCommand cmd, CancellationToken ct = default)
        {
            await _repo.DeleteAsync(cmd.Id, ct);
            await _uow.SaveChangesAsync(ct);
        }
    }

}
