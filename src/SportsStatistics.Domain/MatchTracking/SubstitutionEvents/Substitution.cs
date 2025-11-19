using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.MatchTracking.SubstitutionEvents;

public sealed record Substitution
{
    private Substitution(Guid playerOutId, Guid playerInId)
    {
        PlayerOutId = playerOutId;
        PlayerInId = playerInId;
    }

    public Guid PlayerOutId { get; private set; } = default!;

    public Guid PlayerInId { get; private set; } = default!;

    public static Result<Substitution> Create(Guid? playerOutId, Guid? playerInId)
    {
        if (playerOutId is null || playerOutId == Guid.Empty)
        {
            return MatchTrackingErrors.SubstitutionEvent.PlayerOutId.NullOrEmpty;
        }

        if (playerInId is null || playerInId == Guid.Empty)
        {
            return MatchTrackingErrors.SubstitutionEvent.PlayerInId.NullOrEmpty;
        }

        if (playerOutId == playerInId)
        {
            return MatchTrackingErrors.SubstitutionEvent.SamePlayer;
        }

        return new Substitution(playerOutId.Value, playerInId.Value);
    }
}
