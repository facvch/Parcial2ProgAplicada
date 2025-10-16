using Core.Domain.Validators;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class PlayerValidator : EntityValidator<Player>
    {
        public PlayerValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("El apellido es obligatorio.");
            RuleFor(p => p.Age).InclusiveBetween(0, 120).WithMessage("La edad debe estar entre 0 y 120 años.");
            RuleFor(p => p.RegistrationDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La fecha de registro no puede ser en el futuro.");
        }
    }
}
