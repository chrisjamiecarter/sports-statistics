using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Domain.MatchTracking.PlayerEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.MatchTracking.PlayerEvents;

internal sealed class PlayerEventConfiguration : IEntityTypeConfiguration<PlayerEvent>
{
    public void Configure(EntityTypeBuilder<PlayerEvent> builder)
    {
        builder.ToTable(Schemas.PlayerEvents.Table, Schemas.PlayerEvents.Schema);

        builder.HasKey(playerEvent => playerEvent.Id);

        builder.Property(playerEvent => playerEvent.Id)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(playerEvent => playerEvent.FixtureId)
               .IsRequired();

        builder.ComplexProperty(playerEvent => playerEvent.Minute, propertyBuilder =>
        {
            propertyBuilder.Property(minute => minute.Value)
                           .IsRequired();
        });

        builder.Property(playerEvent => playerEvent.OccurredAtUtc)
               .IsRequired();

        builder.Property(playerEvent => playerEvent.PlayerId)
               .IsRequired();

        builder.Property(playerEvent => playerEvent.Type)
               .IsRequired();

        builder.Property(playerEvent => playerEvent.DeletedOnUtc);

        builder.Property(playerEvent => playerEvent.Deleted)
               .HasDefaultValue(false);

        builder.HasOne<Fixture>()
               .WithMany()
               .HasForeignKey(playerEvent => playerEvent.FixtureId);

        builder.HasOne<Player>()
               .WithMany()
               .HasForeignKey(playerEvent => playerEvent.PlayerId);

        builder.HasQueryFilter(playerEvent => !playerEvent.Deleted);
    }
}
