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

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(f => f.SeasonId)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired();

        builder.Property(p => p.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(p => p.Type)
               .HasConversion(Converters.CompetitionTypeConverter)
               .HasMaxLength(CompetitionType.MaxLength)
               .IsRequired();

        builder.HasOne<Season>()
               .WithMany()
               .HasForeignKey(p => p.SeasonId);
    }
}
