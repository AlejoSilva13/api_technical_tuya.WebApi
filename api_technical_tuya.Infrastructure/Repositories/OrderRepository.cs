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
    public sealed class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db) => _db = db;

        public async Task<Order?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _db.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id, ct);

        public async Task AddAsync(Order order, CancellationToken ct = default)
            => await _db.Orders.AddAsync(order, ct);
    }
}
