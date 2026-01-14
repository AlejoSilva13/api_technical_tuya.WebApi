using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.UseCases.Customer.CreateCustomer
{
    public sealed record CreateCustomerCommand(string Name, string Email);
}
