using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Infrastructure.Persistence.Schemas;

namespace SportsStatistics.Infrastructure.Persistence.Configurations;

internal sealed class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
    {
        builder.ToTable(IdentitySchema.IdentityUserToken.Table, IdentitySchema.IdentityUserToken.Schema);
    }
}
