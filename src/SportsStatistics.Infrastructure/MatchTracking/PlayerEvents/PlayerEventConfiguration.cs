using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;

namespace SportsStatistics.Infrastructure.MatchTracking.MatchEvents;

internal sealed class PlayerEventConfiguration : IEntityTypeConfiguration<PlayerEvent>
{
    public void Configure(EntityTypeBuilder<PlayerEvent> builder)
    {
        builder.ToTable(Schemas.PlayerEvents.Table, Schemas.PlayerEvents.Schema);

        builder.HasKey(playerEvent => playerEvent.Id);

        builder.Property(playerEvent => playerEvent.Id)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(playerEvent => playerEvent.FixtureId)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired();

        builder.Property(playerEvent => playerEvent.Minute)
               .IsRequired();

        builder.Property(playerEvent => playerEvent.OccurredAtUtc)
               .IsRequired();

        builder.Property(playerEvent => playerEvent.PlayerId)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired();

        builder.Property(playerEvent => playerEvent.Type)
               .HasConversion(Converters.PlayerEventTypeConverter)
               .HasMaxLength(PlayerEventType.MaxLength)
               .IsRequired();

        builder.HasOne<Fixture>()
               .WithMany()
               .HasForeignKey(playerEvent => playerEvent.FixtureId);

        builder.HasOne<Player>()
               .WithMany()
               .HasForeignKey(playerEvent => playerEvent.PlayerId);
    }
}
