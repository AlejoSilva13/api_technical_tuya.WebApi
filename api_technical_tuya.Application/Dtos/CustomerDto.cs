using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.Dtos
{
    public sealed record CustomerDto(Guid Id, string Name, string Email, DateTime CreatedAtutc);
}
