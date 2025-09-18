using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Web.Admin.Players;

internal sealed class PlayerFormModelValidator : AbstractValidator<PlayerFormModel>
{
    public PlayerFormModelValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.SquadNumber)
            .InclusiveBetween(1, 99);

        RuleFor(c => c.Nationality)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.DateOfBirth)
            .NotEmpty()
            .Must(dob =>
            {
                return dob?.CalculateAge() >= 15;
            })
            .WithMessage("Player must be at least 15 years old.");

        RuleFor(c => c.Position)
            .NotEmpty();
    }
}
