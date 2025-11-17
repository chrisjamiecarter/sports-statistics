using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;

namespace SportsStatistics.Infrastructure.Competitions;

internal sealed class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.ToTable(Schemas.Competitions.Table, Schemas.Competitions.Schema);

        builder.HasKey(competition => competition.Id);

        builder.Property(competition => competition.Id)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(competition => competition.SeasonId)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired();

        builder.Property(competition => competition.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(competition => competition.Type)
               .HasConversion(Converters.CompetitionTypeConverter)
               .HasMaxLength(CompetitionType.MaxLength)
               .IsRequired();

        builder.HasOne<Season>()
               .WithMany()
               .HasForeignKey(competition => competition.SeasonId);
    }
}
