using FluentValidation;
using SportsStatistics.Domain.Players;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Application.Players.LeftClub;

internal sealed class PlayerLeftClubCommandValidator : AbstractValidator<PlayerLeftClubCommand>
{
    public PlayerLeftClubCommandValidator()
    {
        RuleFor(c => c.PlayerId)
            .NotEmpty().WithError(PlayerErrors.PlayerIdIsRequired);
    }
}
