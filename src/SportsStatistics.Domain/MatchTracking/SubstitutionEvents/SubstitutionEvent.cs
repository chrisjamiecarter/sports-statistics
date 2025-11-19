namespace SportsStatistics.Domain.MatchTracking.SubstitutionEvents;

public class SubstitutionEvent : MatchEventBase
{
    private SubstitutionEvent(Guid fixtureId,
                              Substitution substitution,
                              Minute minute,
                              DateTime occurredAtUtc)
        : base(fixtureId, minute, occurredAtUtc)
    {
        Substitution = substitution;
    }

    public Substitution Substitution { get; private set; } = default!;

    public static SubstitutionEvent Create(Guid fixtureId, Substitution substitution, Minute minute, DateTime occurredAtUtc)
    {
        return new(fixtureId, substitution, minute, occurredAtUtc);
    }
}
