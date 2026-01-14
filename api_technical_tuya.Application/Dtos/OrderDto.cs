using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.Dtos
{
    public sealed record OrderDto(Guid Id, Guid CustomerId, decimal Total, string Status, DateTime CreatedAtUtc);
}
