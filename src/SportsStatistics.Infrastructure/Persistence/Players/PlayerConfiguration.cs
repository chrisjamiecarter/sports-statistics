using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Persistence.Schemas;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Persistence.Players;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable(SportsStatisticsSchema.Players.Table, SportsStatisticsSchema.Players.Schema);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasConversion(id => id.Value, value => EntityId.Create(value))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(p => p.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(p => p.SquadNumber)
               .IsRequired();

        builder.Property(p => p.Nationality)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(p => p.DateOfBirth)
               .HasColumnType("date")
               .IsRequired();

        builder.Property(p => p.Position)
               .HasConversion(v => v.Name, v => Position.FromName(v))
               .IsRequired();

        builder.Ignore(p => p.Age);
    }
}
