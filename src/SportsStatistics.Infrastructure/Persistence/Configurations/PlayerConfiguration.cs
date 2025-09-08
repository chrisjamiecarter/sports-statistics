using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Entities;
using SportsStatistics.Infrastructure.Persistence.Schemas;

namespace SportsStatistics.Infrastructure.Persistence.Configurations;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable(SportsSchema.Players.Table, SportsSchema.Players.Schema);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Role)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(p => p.SquadNumber).IsUnique();

        builder.Property(p => p.Nationality)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.DateOfBirth)
               .IsRequired()
               .HasColumnType("date");

        builder.Ignore(p => p.Age);
    }
}
