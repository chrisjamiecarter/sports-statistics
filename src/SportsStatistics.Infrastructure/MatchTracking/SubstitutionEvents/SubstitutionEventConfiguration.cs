using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.MatchTracking.SubstitutionEvents;
using SportsStatistics.Domain.Players;
using SportsStatistics.Infrastructure.Database;

namespace SportsStatistics.Infrastructure.MatchTracking.SubstitutionEvents;

internal sealed class SubstitutionEventConfiguration : IEntityTypeConfiguration<SubstitutionEvent>
{
    public void Configure(EntityTypeBuilder<SubstitutionEvent> builder)
    {
        builder.ToTable(Schemas.SubstitutionEvents.Table, Schemas.SubstitutionEvents.Schema);

        builder.HasKey(substitutionEvent => substitutionEvent.Id);

        builder.Property(substitutionEvent => substitutionEvent.Id)
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(substitutionEvent => substitutionEvent.FixtureId)
               .IsRequired();

        builder.Property(substitutionEvent => substitutionEvent.Minute)
               .IsRequired();

        builder.Property(substitutionEvent => substitutionEvent.OccurredAtUtc)
               .IsRequired();

        builder.ComplexProperty(substitutionEvent => substitutionEvent.Substitution, propertyBuilder =>
        {
            propertyBuilder.Property(substitution => substitution.PlayerOffId)
                           .IsRequired();

            propertyBuilder.Property(substitution => substitution.PlayerOnId)
                           .IsRequired();
        });

        builder.Property(substitutionEvent => substitutionEvent.DeletedOnUtc);

        builder.Property(substitutionEvent => substitutionEvent.Deleted)
               .HasDefaultValue(false);

        builder.HasOne<Player>()
               .WithMany()
               .HasForeignKey(substitutionEvent => substitutionEvent.Substitution.PlayerOffId);

        builder.HasOne<Player>()
               .WithMany()
               .HasForeignKey(substitutionEvent => substitutionEvent.Substitution.PlayerOnId);

        builder.HasQueryFilter(substitutionEvent => !substitutionEvent.Deleted);
    }
}
