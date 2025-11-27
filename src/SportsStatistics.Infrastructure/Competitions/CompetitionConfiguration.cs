using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Competitions;

internal sealed class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.ToTable(Schemas.Competitions.Table, Schemas.Competitions.Schema);

        builder.HasKey(competition => competition.Id);

        builder.Property(competition => competition.Id)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(competition => competition.SeasonId)
               .IsRequired();

        builder.ComplexProperty(competition => competition.Name, propertyBuilder =>
        {
            propertyBuilder.Property(name => name.Value)
                           .HasMaxLength(Name.MaxLength)
                           .IsRequired();
        });

        builder.Property(competition => competition.Format)
               .IsRequired();

        builder.Property(competition => competition.DeletedOnUtc);

        builder.Property(competition => competition.Deleted)
               .HasDefaultValue(false);

        builder.HasOne<Season>()
               .WithMany()
               .HasForeignKey(competition => competition.SeasonId);

        builder.HasQueryFilter(competition => !competition.Deleted);
    }
}
