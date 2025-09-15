using FluentValidation;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Create;

public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator(IPlayerRepository repository)
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.SquadNumber)
            .InclusiveBetween(1, 99)
            .MustAsync(async (squadNumber, cancellation) =>
            {
                var available = await repository.IsSquadNumberAvailableAsync(squadNumber, null, cancellation);
                return available;
            })
            .WithMessage("Squad number is already taken by another player.");

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

        RuleFor(c => c.Position)
            .NotEmpty()
            .Must(position =>
            {
                return Position.All.Any(p => string.Equals(p.Name, position, StringComparison.OrdinalIgnoreCase));
            })
            .WithMessage($"Invalid position. Valid positions: {string.Join(", ", Position.All)}");
    }
}
