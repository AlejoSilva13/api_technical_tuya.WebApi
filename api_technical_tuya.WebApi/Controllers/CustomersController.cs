using api_technical_tuya.Application.Dtos;
using api_technical_tuya.Application.UseCases.Customer.CreateCustomer;
using api_technical_tuya.Application.UseCases.Customer.DeleteCustomer;
using api_technical_tuya.Application.UseCases.Customer.GetCustomer;
using api_technical_tuya.Application.UseCases.Customer.UpdateCustomer;
using api_technical_tuya.Application.UseCases.Customers.CreateCustomer;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace api_technical_tuya.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class CustomersController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CustomerDto>>> GetAll(
            [FromServices] GetAllCustomersHandler handler,
            CancellationToken ct)
        {
            var result = await handler.HandleGetAllAsync(new GetAllCustomersQuery(), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerDto>> GetById(
            Guid id,
            [FromServices] GetCustomerIdHandler handler,
            CancellationToken ct)
        {
            var result = await handler.HandleGetIdAsync(new GetCustomerByIdQuery(id), ct);
            return Ok(result);
        }

        public sealed record CreateCustomerRequest(string Name, string Email);

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create(
            [FromBody] CreateCustomerRequest req,
            [FromServices] CreateCustomerHandler handler,
            CancellationToken ct)
        {
            var created = await handler.HandleCreateAsync(new CreateCustomerCommand(req.Name, req.Email), ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        public sealed record UpdateCustomerRequest(string Name, string Email);

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateCustomerRequest req,
            [FromServices] UpdateCustomerHandler handler,
            CancellationToken ct)
        {
            var updated = await handler.HandleUpdateAsync(new UpdateCustomerCommand(id, req.Name, req.Email), ct);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(
            Guid id,
            [FromServices] DeleteCustomerHandler handler,
            CancellationToken ct)
        {
            await handler.HandleDeleteAsync(new DeleteCustomerCommand(id), ct);
            return NoContent();
        }
    }
}
