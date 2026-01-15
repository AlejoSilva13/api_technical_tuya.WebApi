using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Domain.Entities
{
    public sealed class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public string Status { get; private set; } = "Created";
        public DateTime CreatedAtUtc { get; private set; }

        private Order() { } 

        public Order(Guid customerId, decimal total, DateTime createdAtUtc)
        {
            if (customerId == Guid.Empty) throw new ArgumentException("CustomerId is required");
            if (total < 0) throw new ArgumentException("Total cannot be negative");

            Id = Guid.NewGuid();
            CustomerId = customerId;
            Total = total;
            CreatedAtUtc = createdAtUtc;
        }
    }
}
