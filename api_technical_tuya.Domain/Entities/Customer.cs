using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Domain.Entities
{
    public sealed class Customer
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public DateTime CreatedAtUtc { get; private set; }

        private Customer() { } 

        public Customer(Guid id, string name, string email, DateTime createdAtUtc)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required");

            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Name = name.Trim();
            Email = email.Trim().ToLowerInvariant();
            CreatedAtUtc = createdAtUtc;
        }

        public void Update(string name, string email)
        {
            if (!string.IsNullOrWhiteSpace(name)) Name = name.Trim();
            if (!string.IsNullOrWhiteSpace(email)) Email = email.Trim().ToLowerInvariant();
        }

    }
}
