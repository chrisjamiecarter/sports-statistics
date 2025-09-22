using FluentValidation;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Seasons.Update;

internal sealed class UpdateSeasonCommandValidator : AbstractValidator<UpdateSeasonCommand>
{
    public UpdateSeasonCommandValidator(ISeasonRepository repository)
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .Must(guid => guid.Version == 7);

        RuleFor(c => c.StartDate)
            .NotEmpty()
            .LessThan(c => c.EndDate)
            .MustAsync(async (command, start, cancellation) =>
            {
                var entityId = EntityId.Create(command.Id).Value;
                var hasOverlap = await repository.DoesDateOverlapExistingAsync(start, entityId, cancellation);
                return !hasOverlap;
            })
            .WithMessage("'Start Date' overlaps with an existing season.");


        RuleFor(c => c.EndDate)
            .NotEmpty()
            .GreaterThan(c => c.StartDate)
            .MustAsync(async (command, end, cancellation) =>
            {
                var entityId = EntityId.Create(command.Id).Value;
                var hasOverlap = await repository.DoesDateOverlapExistingAsync(end, entityId, cancellation);
                return !hasOverlap;
            })
            .WithMessage("'End Date' overlaps with an existing season.");
    }
}
