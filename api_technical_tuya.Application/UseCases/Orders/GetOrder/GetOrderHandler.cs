using api_technical_tuya.Application.Dtos;
using api_technical_tuya.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_technical_tuya.Application.UseCases.Orders.GetOrder
{
    public sealed class GetOrderHandler
    {

        private readonly IOrderRepository _orders;

        public GetOrderHandler(IOrderRepository orders)
            => _orders = orders;

        public async Task<OrderDto> HandleAsync(GetOrderIdCommand query, CancellationToken ct = default)
        {
            var order = await _orders.GetByIdAsync(query.Id, ct)
                ?? throw new InvalidOperationException("Order not found");

            return new OrderDto(order.Id, order.CustomerId, order.Total, order.Status, order.CreatedAtUtc);
        }

    }
}
