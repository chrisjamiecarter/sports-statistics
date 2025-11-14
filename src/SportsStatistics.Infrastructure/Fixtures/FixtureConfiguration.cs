using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;

namespace SportsStatistics.Infrastructure.Fixtures;

internal sealed class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
{
    public void Configure(EntityTypeBuilder<Fixture> builder)
    {
        builder.ToTable(Schemas.Fixtures.Table, Schemas.Fixtures.Schema);

        builder.HasKey(fixture => fixture.Id);

        builder.Property(fixture => fixture.Id)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(fixture => fixture.CompetitionId)
               .HasConversion(Converters.EntityIdConverter)
               .IsRequired();

        builder.Property(fixture => fixture.Opponent)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(fixture => fixture.KickoffTimeUtc)
               .IsRequired();

        builder.Property(fixture => fixture.Location)
               .HasConversion(v => v.Name, v => FixtureLocation.FromName(v))
               .HasMaxLength(FixtureLocation.All.Max(s => s.Name.Length))
               .IsRequired();

        builder.OwnsOne(fixture => fixture.Score, score =>
        {
            score.Property(fixtureScore => fixtureScore.HomeGoals)
                 .IsRequired();
            score.Property(fixtureScore => fixtureScore.AwayGoals)
                 .IsRequired();
            score.WithOwner();
        });

        builder.Property(fixture => fixture.Status)
               .HasConversion(Converters.FixtureStatusConverter)
               .HasMaxLength(FixtureStatus.MaxLength)
               .IsRequired();

        builder.HasOne<Competition>()
               .WithMany()
               .HasForeignKey(fixture => fixture.CompetitionId);
    }
}
