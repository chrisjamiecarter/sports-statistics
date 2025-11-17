using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;

namespace SportsStatistics.Infrastructure.MatchTracking.SubstitutionEvents;

internal sealed class SubstitutionEventConfiguration : IEntityTypeConfiguration<SubstitutionEvent>
{
    public void Configure(EntityTypeBuilder<SubstitutionEvent> builder)
    {
        builder.ToTable(Schemas.SubstitutionEvents.Table, Schemas.SubstitutionEvents.Schema);

        builder.HasKey(substitutionEvent => substitutionEvent.Id);

        builder.Property(substitutionEvent => substitutionEvent.Id)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(substitutionEvent => substitutionEvent.FixtureId)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired();

        builder.Property(substitutionEvent => substitutionEvent.Minute)
               .IsRequired();

        builder.Property(substitutionEvent => substitutionEvent.OccurredAtUtc)
               .IsRequired();

        builder.Property(substitutionEvent => substitutionEvent.PlayerOutId)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired();

        builder.Property(substitutionEvent => substitutionEvent.PlayerInId)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired();

        builder.HasOne<Player>()
               .WithMany()
               .HasForeignKey(substitutionEvent => substitutionEvent.PlayerOutId);

        builder.HasOne<Player>()
               .WithMany()
               .HasForeignKey(substitutionEvent => substitutionEvent.PlayerInId);
    }
}
