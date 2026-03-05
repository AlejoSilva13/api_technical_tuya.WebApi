using api_technical_tuya.Application.Dtos;
using api_technical_tuya.Domain.Interfaces;

namespace api_technical_tuya.Application.UseCases.Orders.GetAllOrders
{
    public sealed record GetAllOrdersQuery();

    public sealed class GetAllOrdersHandler
    {
        private readonly IOrderRepository _orders;

        public GetAllOrdersHandler(IOrderRepository orders)
        {
            _orders = orders;
        }

        public async Task<IReadOnlyList<OrderDto>> HandleAsync(GetAllOrdersQuery query, CancellationToken ct = default)
        {
            var orders = await _orders.GetAllAsync(ct);
            return orders.Select(o => new OrderDto(o.Id, o.CustomerId, o.Total, o.Status, o.CreatedAtUtc)).ToList();
        }
    }
}
