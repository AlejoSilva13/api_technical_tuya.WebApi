using api_technical_tuya.WebApi.Controllers;
using FluentValidation;

namespace api_technical_tuya.WebApi.Validators
{
    public sealed class CreateCustomerRequestValidator : AbstractValidator<CustomersController.CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("Debe ser un email válido.");
        }
    }
}
