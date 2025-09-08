using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Infrastructure.Persistence.Schemas;

namespace SportsStatistics.Infrastructure.Persistence.Configurations;

internal sealed class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.ToTable(IdentitySchema.IdentityUserRole.Table, IdentitySchema.IdentityUserRole.Schema);
    }
}
