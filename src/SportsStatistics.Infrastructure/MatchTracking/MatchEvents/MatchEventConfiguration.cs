using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;

namespace SportsStatistics.Infrastructure.MatchTracking.MatchEvents;

internal sealed class MatchEventConfiguration : IEntityTypeConfiguration<MatchEvent>
{
    public void Configure(EntityTypeBuilder<MatchEvent> builder)
    {
        builder.ToTable(Schemas.MatchEvents.Table, Schemas.MatchEvents.Schema);

        builder.HasKey(matchEvent => matchEvent.Id);

        builder.Property(matchEvent => matchEvent.Id)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(matchEvent => matchEvent.FixtureId)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired();

        builder.Property(matchEvent => matchEvent.Minute)
               .IsRequired();

        builder.Property(matchEvent => matchEvent.OccurredAtUtc)
               .IsRequired();

        builder.Property(matchEvent => matchEvent.Type)
               .HasConversion(Converters.MatchEventTypeConverter)
               .HasMaxLength(MatchEventType.MaxLength)
               .IsRequired();

        builder.HasOne<Fixture>()
               .WithMany()
               .HasForeignKey(matchEvent => matchEvent.FixtureId);
    }
}
