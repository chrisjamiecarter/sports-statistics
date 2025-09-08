using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Infrastructure.Persistence.Models;
using SportsStatistics.Infrastructure.Persistence.Schemas;

namespace SportsStatistics.Infrastructure.Persistence.Configurations;

internal sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable(IdentitySchema.ApplicationUser.Table, IdentitySchema.ApplicationUser.Schema);
    }
}
