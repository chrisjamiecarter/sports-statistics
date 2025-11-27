using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Competitions;
using SportsStatistics.Domain.Fixtures;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.Fixtures;

internal sealed class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
{
    public void Configure(EntityTypeBuilder<Fixture> builder)
    {
        builder.ToTable(Schemas.Fixtures.Table, Schemas.Fixtures.Schema);

        builder.HasKey(fixture => fixture.Id);

        builder.Property(fixture => fixture.Id)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(fixture => fixture.CompetitionId)
               .IsRequired();

        builder.ComplexProperty(fixture => fixture.Opponent, propertyBuilder =>
        {
            propertyBuilder.Property(opponent => opponent.Value)
                           .HasMaxLength(Opponent.MaxLength)
                           .IsRequired();
        });

        builder.ComplexProperty(fixture => fixture.KickoffTimeUtc, propertyBuilder =>
        {
            propertyBuilder.Property(kickoffTimeUtc => kickoffTimeUtc.Value)
                           .IsRequired();
        });

        builder.Property(fixture => fixture.Location)
               .IsRequired();

        builder.ComplexProperty(fixture => fixture.Score, propertyBuilder =>
        {
            propertyBuilder.Property(score => score.HomeGoals)
                           .IsRequired();

            propertyBuilder.Property(score => score.AwayGoals)
                           .IsRequired();
        });

        builder.Property(fixture => fixture.Status)
               .IsRequired();

        builder.Property(fixture => fixture.DeletedOnUtc);

        builder.Property(fixture => fixture.Deleted)
               .HasDefaultValue(false);

        builder.HasOne<Competition>()
               .WithMany()
               .HasForeignKey(fixture => fixture.CompetitionId);

        builder.HasQueryFilter(fixture => !fixture.Deleted);
    }
}
