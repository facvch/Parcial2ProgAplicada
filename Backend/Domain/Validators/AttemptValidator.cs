using Core.Domain.Validators;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class AttemptValidator : EntityValidator<Attempt>
    {
        public AttemptValidator()
        {
            RuleFor(x => x.GameId).GreaterThan(0).WithMessage("GameId debe ser mayor a 0.");
            RuleFor(x => x.AttemptedNumber)
                .NotEmpty().WithMessage("AttemptedNumber no puede ser 0.")
                .Matches(@"^\d{4}$").WithMessage("AttemptedNumber debe ser un numero de 4 digitos.");
            RuleFor(x => x.AttemptDate).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("AttemptDate no puede estar en una fecha futura.");
            RuleFor(x => x.Result).NotEmpty().WithMessage("Result no puede estar vacio.");
        }
    }
}
