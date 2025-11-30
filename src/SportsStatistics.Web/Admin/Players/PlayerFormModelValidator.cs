using FluentValidation;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Admin.Players;

internal sealed class PlayerFormModelValidator : AbstractValidator<PlayerFormModel>
{
    public PlayerFormModelValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(Name.MaxLength);

        RuleFor(c => c.SquadNumber)
            .InclusiveBetween(SquadNumber.MinValue, SquadNumber.MaxValue);

        RuleFor(c => c.Nationality)
            .NotEmpty()
            .MaximumLength(Nationality.MaxLength);

        RuleFor(c => c.DateOfBirth)
            .NotEmpty()
            .Must(dob =>
            {
                return dob?.CalculateAge() >= DateOfBirth.MinAge;
            })
            .WithMessage("Player must be at least 15 years old.");

        RuleFor(c => c.Position)
            .NotEmpty();
    }
}
