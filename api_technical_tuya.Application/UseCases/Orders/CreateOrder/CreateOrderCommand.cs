using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.UseCases.Orders.CreateOrder
{
    public sealed record CreateOrderCommand(Guid CustomerId, decimal Total);
}
