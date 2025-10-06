using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Seasons;

internal sealed class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable(Schemas.Seasons.Table, Schemas.Seasons.Schema);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasConversion(id => id.Value, value => EntityId.Create(value))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(p => p.StartDate)
               .HasColumnType("date")
               .IsRequired();

        builder.Property(p => p.EndDate)
               .HasColumnType("date")
               .IsRequired();

        builder.Ignore(p => p.Name);
    }
}
