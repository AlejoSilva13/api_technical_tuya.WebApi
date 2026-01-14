using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.UseCases.Customer.UpdateCustomer
{
    public sealed record UpdateCustomerCommand(Guid Id, string Name, string Email);
}
