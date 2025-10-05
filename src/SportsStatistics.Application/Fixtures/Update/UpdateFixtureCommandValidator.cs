using FluentValidation;
using SportsStatistics.Domain.Fixtures;

namespace SportsStatistics.Application.Fixtures.Update;

internal sealed class UpdateFixtureCommandValidator : AbstractValidator<UpdateFixtureCommand>
{
    public UpdateFixtureCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(guid => guid.Version == 7)
            .WithMessage("'Id' is not in the correct format.");
        
        RuleFor(c => c.CompetitionId)
            .NotEmpty()
            .Must(guid => guid.Version == 7)
            .WithMessage("'Competition Id' is not in the correct format.");

        // TODO: Only one fixture per day?
        RuleFor(c => c.KickoffTimeUtc)
            .NotEmpty();

        RuleFor(c => c.LocationName)
            .NotEmpty()
            .Must(location =>
            {
                return FixtureLocation.All.Any(l => string.Equals(l.Name, location, StringComparison.OrdinalIgnoreCase));
            })
            .WithMessage($"Invalid fixture location. Valid fixture locations: {string.Join(", ", FixtureLocation.All)}.");

        RuleFor(c => c.HomeGoals)
            .GreaterThanOrEqualTo(0);

        RuleFor(c => c.AwayGoals)
            .GreaterThanOrEqualTo(0);

        RuleFor(c => c.StatusName)
            .NotEmpty()
            .Must(status =>
            {
                return FixtureStatus.All.Any(s => string.Equals(s.Name, status, StringComparison.OrdinalIgnoreCase));
            })
            .WithMessage($"Invalid fixture status. Valid fixture statuses: {string.Join(", ", FixtureStatus.All)}.");
    }
}
