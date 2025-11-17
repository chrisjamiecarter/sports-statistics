using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;

namespace SportsStatistics.Infrastructure.Players;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable(Schemas.Players.Table, Schemas.Players.Schema);

        builder.HasKey(player => player.Id);

        builder.Property(player => player.Id)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(player => player.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(player => player.SquadNumber)
               .IsRequired();

        builder.Property(player => player.Nationality)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(player => player.DateOfBirth)
               .HasColumnType("date")
               .IsRequired();

        builder.Property(player => player.Position)
               .HasConversion(Converters.PlayerPositionConverter)
               .HasMaxLength(Position.MaxLength)
               .IsRequired();

        builder.Ignore(player => player.Age);
    }
}
