using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Infrastructure.Persistence.Schemas;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Persistence.Competitions;

internal sealed class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.ToTable(SportsStatisticsSchema.Competitions.Table, SportsStatisticsSchema.Competitions.Schema);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasConversion(id => id.Value, value => EntityId.Create(value))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(p => p.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(p => p.Type)
               .HasConversion(v => v.Name, v => CompetitionType.FromName(v))
               .IsRequired();
    }
}
