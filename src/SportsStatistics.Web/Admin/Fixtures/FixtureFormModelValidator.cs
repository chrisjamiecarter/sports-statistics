using FluentValidation;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;

namespace SportsStatistics.Web.Admin.Fixtures;

internal sealed class FixtureFormModelValidator : AbstractValidator<FixtureFormModel>
{
    public FixtureFormModelValidator()
    {
        RuleFor(f => f.Season)
            .NotNull();

        RuleFor(f => f.Competition)
            .NotNull();

        RuleFor(f => f.KickoffDateUtc)
            .NotNull()
            .GreaterThanOrEqualTo(model => model.Season!.StartDate.ToDateTime())
            .When(model => model.Season is not null)
            .WithMessage(model => $"'Date' must be within the season '{model.Season!.StartDate:d}' - '{model.Season!.EndDate:d}'.")
            .LessThanOrEqualTo(model => model.Season!.EndDate.ToDateTime())
            .When(model => model.Season is not null)
            .WithMessage(model => $"'Date' must be within the season '{model.Season!.StartDate:d}' - '{model.Season!.EndDate:d}'.");
        RuleFor(f => f.KickoffTimeUtc)
            .NotNull();

        RuleFor(f => f.Location)
            .NotNull();

        RuleFor(c => c.Opponent)
            .NotEmpty()
            .MaximumLength(100);
    }
}