using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking.MatchEvents;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.MatchTracking.MatchEvents;

internal sealed class MatchEventConfiguration : IEntityTypeConfiguration<MatchEvent>
{
    public void Configure(EntityTypeBuilder<MatchEvent> builder)
    {
        builder.ToTable(Schemas.MatchEvents.Table, Schemas.MatchEvents.Schema);

        builder.HasKey(matchEvent => matchEvent.Id);

        builder.Property(matchEvent => matchEvent.Id)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(matchEvent => matchEvent.FixtureId)
               .IsRequired();

        builder.ComplexProperty(matchEvent => matchEvent.Minute, propertyBuilder =>
        {
            propertyBuilder.Property(minute => minute.Value)
                           .IsRequired();
        });

        builder.Property(matchEvent => matchEvent.OccurredAtUtc)
               .IsRequired();

        builder.Property(matchEvent => matchEvent.Type)
               .IsRequired();

        builder.Property(matchEvent => matchEvent.DeletedOnUtc);

        builder.Property(matchEvent => matchEvent.Deleted)
               .HasDefaultValue(false);

        builder.HasOne<Fixture>()
               .WithMany()
               .HasForeignKey(matchEvent => matchEvent.FixtureId);

        builder.HasQueryFilter(matchEvent => !matchEvent.Deleted);
    }
}
