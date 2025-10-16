using Core.Domain.Validators;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class GameValidator : EntityValidator<Game>
    {
        public GameValidator()
        {
            RuleFor(x => x.PlayerId).GreaterThan(0).WithMessage("PlayerId debe ser mayor a 0.");
            RuleFor(x => x.SecretNumber).NotNull().NotEmpty().WithMessage("SecretNumber no puede ser null o vacio.");
            RuleFor(x => x.CreatedAt).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("CreatedAt no puede estar definido en una fecha futura.");
        }
    }
}
