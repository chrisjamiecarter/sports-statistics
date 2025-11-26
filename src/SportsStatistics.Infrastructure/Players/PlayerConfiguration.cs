using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Domain.Players;
using SportsStatistics.Domain.Seasons;
using SportsStatistics.Infrastructure.Database;
using SportsStatistics.Infrastructure.Database.Converters;

namespace SportsStatistics.Infrastructure.Players;

internal sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable(Schemas.Players.Table, Schemas.Players.Schema);

        builder.HasKey(player => player.Id);

        builder.Property(player => player.Id)
               .IsRequired()
               .ValueGeneratedNever();

        builder.ComplexProperty(player => player.Name, propertyBuilder =>
        {
            propertyBuilder.Property(name => name.Value)
                           .HasMaxLength(Name.MaxLength)
                           .IsRequired();
        });

        builder.ComplexProperty(player => player.SquadNumber, propertyBuilder =>
        {
            propertyBuilder.Property(squadNumber => squadNumber.Value)
                           .IsRequired();
        });

        builder.ComplexProperty(player => player.Nationality, propertyBuilder =>
        {
            propertyBuilder.Property(nationality => nationality.Value)
                           .HasMaxLength(Nationality.MaxLength)
                           .IsRequired();
        });

        builder.ComplexProperty(player => player.DateOfBirth, propertyBuilder =>
        {
            propertyBuilder.Property(dateOfBirth => dateOfBirth.Value)
                           //.HasColumnType("date")
                           .IsRequired();
        });

        builder.Property(player => player.Position)
               .IsRequired();

        builder.Ignore(player => player.Age);

        builder.Property(player => player.DeletedOnUtc);

        builder.Property(player => player.Deleted)
               .HasDefaultValue(false);

        builder.HasQueryFilter(player => !player.Deleted);
    }
}
