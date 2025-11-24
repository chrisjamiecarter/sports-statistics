using FluentValidation;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.Delete;

internal sealed class DeletePlayerCommandValidator : AbstractValidator<DeletePlayerCommand>
{
    public DeletePlayerCommandValidator()
    {
        RuleFor(c => c.PlayerId)
            .NotEmpty().WithError(PlayerErrors.PlayerIdIsRequired);
    }
}
