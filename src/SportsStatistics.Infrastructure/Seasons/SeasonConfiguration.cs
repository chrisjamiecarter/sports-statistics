using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Seasons;

internal sealed class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable(Schemas.Seasons.Table, Schemas.Seasons.Schema);

        builder.HasKey(season => season.Id);

        builder.Property(season => season.Id)
               .IsRequired()
               .ValueGeneratedNever();

        builder.ComplexProperty(season => season.DateRange, propertyBuilder =>
        {
            propertyBuilder.Property(dateRange => dateRange.StartDate)
                           //.HasColumnType("date")
                           .IsRequired();

            propertyBuilder.Property(dateRange => dateRange.EndDate)
                           //.HasColumnType("date")
                           .IsRequired();
        });

        builder.Ignore(season => season.Name);

        builder.Property(season => season.DeletedOnUtc);

        builder.Property(season => season.Deleted)
               .HasDefaultValue(false);

        builder.HasQueryFilter(season => !season.Deleted);
    }
}
