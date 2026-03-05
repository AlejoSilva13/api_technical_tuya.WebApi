using api_technical_tuya.WebApi.Controllers;
using FluentValidation;
using System.Text.RegularExpressions;

namespace api_technical_tuya.WebApi.Validators
{
    public sealed class CreateCustomerRequestValidator : AbstractValidator<CustomersController.CreateCustomerRequest>
    {
       
        private static readonly Regex ValidNameRegex = new(
            @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s\-']+$",
            RegexOptions.Compiled);

        public CreateCustomerRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(60).WithMessage("El nombre no puede superar los 60 caracteres.")
                .Must(BeValidName).WithMessage("El nombre solo puede contener letras, tildes, espacios y guiones.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("Debe ser un email válido.")
                .MaximumLength(100).WithMessage("El email no puede superar los 100 caracteres.");
        }

        private static bool BeValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return true; 

            return ValidNameRegex.IsMatch(name);
        }
    }
}