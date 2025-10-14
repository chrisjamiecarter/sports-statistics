using FluentValidation;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.Create;

internal sealed class CreateFixtureCommandValidator : AbstractValidator<CreateFixtureCommand>
{
    public CreateFixtureCommandValidator()
    {
        RuleFor(c => c.CompetitionId)
            .NotEmpty()
            .Must(guid => guid.Version == 7)
            .WithMessage("'Competition Id' is not in the correct format.");

        RuleFor(c => c.Opponent)
            .NotEmpty()
            .MaximumLength(100);

        // TODO: Only one fixture per day?
        RuleFor(c => c.KickoffTimeUtc)
            .NotEmpty();

        RuleFor(c => c.FixtureLocationName)
            .NotEmpty()
            .MaximumLength(7)
            .Must(location =>
            {
                return FixtureLocation.All.Any(l => string.Equals(l.Name, location, StringComparison.OrdinalIgnoreCase));
            })
            .WithMessage($"Invalid fixture location. Valid fixture locations: {string.Join(", ", FixtureLocation.All)}.");
    }
}
