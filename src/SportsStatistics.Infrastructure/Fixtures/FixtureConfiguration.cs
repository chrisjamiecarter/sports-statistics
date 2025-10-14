using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.SharedKernel;

namespace SportsStatistics.Infrastructure.Fixtures;

internal sealed class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
{
    public void Configure(EntityTypeBuilder<Fixture> builder)
    {
        builder.ToTable(Schemas.Fixtures.Table, Schemas.Fixtures.Schema);

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
               .HasConversion(id => id.Value, value => EntityId.Create(value))
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(f => f.CompetitionId)
               .HasConversion(id => id.Value, value => EntityId.Create(value))
               .IsRequired();

        builder.Property(f => f.Opponent)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(f => f.KickoffTimeUtc)
               .IsRequired();

        builder.Property(f => f.Location)
               .HasConversion(v => v.Name, v => FixtureLocation.FromName(v))
               .HasMaxLength(FixtureLocation.All.Max(s => s.Name.Length))
               .IsRequired();

        builder.OwnsOne(f => f.Score, score =>
        {
            score.Property(s => s.HomeGoals)
                 .IsRequired();
            score.Property(s => s.AwayGoals)
                 .IsRequired();
            score.WithOwner();
        });

        builder.Property(f => f.Status)
               .HasConversion(v => v.Name, v => FixtureStatus.FromName(v))
               .HasMaxLength(FixtureStatus.All.Max(s => s.Name.Length))
               .IsRequired();

        builder.HasOne<Competition>()
               .WithMany()
               .HasForeignKey(f => f.CompetitionId);
    }
}
