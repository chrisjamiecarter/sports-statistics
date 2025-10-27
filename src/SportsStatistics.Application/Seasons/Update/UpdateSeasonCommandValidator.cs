﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SportsStatistics.Application.Abstractions.Data;

namespace SportsStatistics.Application.Seasons.Update;

internal sealed class UpdateSeasonCommandValidator : AbstractValidator<UpdateSeasonCommand>
{
    public UpdateSeasonCommandValidator(IApplicationDbContext dbContext)
    {
        RuleFor(c => c.SeasonId)
            .NotEmpty()
            .Must(guid => guid.Version == 7)
            .WithMessage("'Season Id' is not in the correct format.");

        RuleFor(c => c.StartDate)
            .NotEmpty()
            .LessThan(c => c.EndDate)
            .MustAsync(async (command, start, cancellation) =>
            {
                var hasOverlap = await DoesDateOverlapExistingAsync(dbContext, start, command.SeasonId, cancellation);
                return !hasOverlap;
            })
            .WithMessage("'Start Date' overlaps with an existing season.");


        RuleFor(c => c.EndDate)
            .NotEmpty()
            .GreaterThan(c => c.StartDate)
            .MustAsync(async (command, end, cancellation) =>
            {
                var hasOverlap = await DoesDateOverlapExistingAsync(dbContext, end, command.SeasonId, cancellation);
                return !hasOverlap;
            })
            .WithMessage("'End Date' overlaps with an existing season.");
    }

    private static async Task<bool> DoesDateOverlapExistingAsync(IApplicationDbContext dbContext, DateOnly date, Guid excludeSeasonId, CancellationToken cancellationToken)
    {
        var overlappingSeasons = await dbContext.Seasons.Where(season => season.Id != excludeSeasonId && season.StartDate <= date && season.EndDate >= date)
                                                        .ToListAsync(cancellationToken);

        return overlappingSeasons.Count > 0;
    }
}
