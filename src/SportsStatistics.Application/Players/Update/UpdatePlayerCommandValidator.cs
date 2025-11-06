using FluentValidation;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Update;

internal sealed class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator()
    {
        RuleFor(c => c.PlayerId)
            .NotEmpty();

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
                return dob.CalculateAge() >= 15;
            })
            .WithMessage("Player must be at least 15 years old.");

        RuleFor(c => c.PositionName)
            .NotEmpty()
            .Must(position =>
            {
                return Position.All.Any(p => string.Equals(p.Name, position, StringComparison.OrdinalIgnoreCase));
            })
            .WithMessage($"Invalid position. Valid positions: {string.Join(", ", Position.All)}.");
    }
}
