using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;

namespace SportsStatistics.Infrastructure.Seasons;

internal sealed class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable(Schemas.Seasons.Table, Schemas.Seasons.Schema);

        builder.HasKey(season => season.Id);

        builder.Property(season => season.Id)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(season => season.StartDate)
               .HasColumnType("date")
               .IsRequired();

        builder.Property(season => season.EndDate)
               .HasColumnType("date")
               .IsRequired();

        builder.Ignore(season => season.Name);
    }
}
