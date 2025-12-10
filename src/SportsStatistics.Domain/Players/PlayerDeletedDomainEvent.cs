using SportsStatistics.SharedKernel;

namespace SportsStatistics.Domain.Players;

public sealed record PlayerDeletedDomainEvent(Guid Id) : IDomainEvent;
