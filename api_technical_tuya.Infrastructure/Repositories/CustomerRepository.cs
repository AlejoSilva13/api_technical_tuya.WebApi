using api_technical_tuya.Domain.Entities;
using api_technical_tuya.Domain.Interfaces;
using api_technical_tuya.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Infrastructure.Repositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _db;
        public CustomerRepository(AppDbContext db) => _db = db;

        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _db.Customers.FindAsync(new object[] { id }, ct);

        public async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken ct = default)
            => await _db.Customers.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);

        public async Task AddAsync(Customer customer, CancellationToken ct = default)
            => await _db.Customers.AddAsync(customer, ct);

        public Task UpdateAsync(Customer customer, CancellationToken ct = default)
        {
            _db.Customers.Update(customer);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _db.Customers.FindAsync(new object[] { id }, ct);
            if (entity is null) return;
            _db.Customers.Remove(entity);
        }
    }
}
