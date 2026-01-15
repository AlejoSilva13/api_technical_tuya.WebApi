using api_technical_tuya.Application.Dtos;
using api_technical_tuya.Application.UseCases.Orders.CreateOrder;
using api_technical_tuya.Application.UseCases.Orders.GetOrder;
using Microsoft.AspNetCore.Mvc;

namespace api_technical_tuya.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class OrdersController : ControllerBase
    {
        public sealed record CreateOrderRequest(Guid CustomerId, decimal Total);

        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CreateOrderRequest req,
            [FromServices] CreateOrderHandler handler,
            CancellationToken ct)
        {
            var result = await handler.HandleCreateAsync(new CreateOrderCommand(req.CustomerId, req.Total), ct);
            return CreatedAtAction(nameof(GetById), new { id = result.OrderId }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetById(
            Guid id,
            [FromServices] GetOrderHandler handler,
            CancellationToken ct)
        {
            var dto = await handler.HandleAsync(new GetOrderIdCommand(id), ct);
            return Ok(dto);
        }
    }
}
