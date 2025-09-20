using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Persistence.Schemas;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Persistence.Seasons;

internal sealed class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable(SportsStatisticsSchema.Seasons.Table, SportsStatisticsSchema.Seasons.Schema);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasConversion(id => id.Value, value => new EntityId(value))
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
