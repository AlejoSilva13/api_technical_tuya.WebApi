using api_technical_tuya.WebApi.Controllers;
using FluentValidation;

namespace api_technical_tuya.WebApi.Validators
{
    public sealed class CreateOrderRequestValidator : AbstractValidator<OrdersController.CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("El CustomerId es obligatorio.")
                .NotEqual(Guid.Empty).WithMessage("El CustomerId no puede ser un GUID vacío.");

            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("El total debe ser mayor a 0.")
                .LessThanOrEqualTo(999999999.99m).WithMessage("El total excede el límite permitido.");
        }
    }
}