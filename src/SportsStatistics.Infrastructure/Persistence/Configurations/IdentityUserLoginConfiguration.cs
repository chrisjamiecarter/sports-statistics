using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStatistics.Infrastructure.Persistence.Schemas;

namespace SportsStatistics.Infrastructure.Persistence.Configurations;

internal sealed class IdentityUserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
    {
        builder.ToTable(IdentitySchema.IdentityUserLogin.Table, IdentitySchema.IdentityUserLogin.Schema);
    }
}
