using FluentValidation;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.RejoinedClub;

internal sealed class PlayerRejoinedClubCommandValidator : AbstractValidator<PlayerRejoinedClubCommand>
{
    public PlayerRejoinedClubCommandValidator()
    {
        RuleFor(c => c.PlayerId)
            .NotEmpty().WithError(PlayerErrors.PlayerIdIsRequired);
    }
}
