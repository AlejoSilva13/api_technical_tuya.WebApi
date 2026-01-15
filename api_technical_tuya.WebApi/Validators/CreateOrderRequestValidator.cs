using api_technical_tuya.WebApi.Controllers;
using FluentValidation;

namespace api_technical_tuya.WebApi.Validators
{
    public sealed class CreateOrderRequestValidator : AbstractValidator<OrdersController.CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId es obligatorio.");

            RuleFor(x => x.Total)
                .GreaterThan(0M).WithMessage("El total debe ser mayor a cero.");
        }
    }
}
