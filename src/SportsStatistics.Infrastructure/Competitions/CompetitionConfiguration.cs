using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Competitions;

internal sealed class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.ToTable(Schemas.Competitions.Table, Schemas.Competitions.Schema);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .HasConversion(id => id.Value, value => EntityId.Create(value))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(f => f.SeasonId)
               .HasConversion(id => id.Value, value => EntityId.Create(value))
               .IsRequired();

        builder.Property(p => p.Name)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(p => p.Type)
               .HasConversion(v => v.Name, v => CompetitionType.FromName(v))
               .HasMaxLength(CompetitionType.All.Max(t => t.Name.Length))
               .IsRequired();

        builder.HasOne<Season>()
               .WithMany()
               .HasForeignKey(p => p.SeasonId);
    }
}
