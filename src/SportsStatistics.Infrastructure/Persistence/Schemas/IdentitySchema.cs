namespace SportsStatistics.Infrastructure.Persistence.Schemas;

internal static class IdentitySchema
{
    private const string Schema = "identity";

    public static readonly SchemaMetadata ApplicationUser = new(Schema, "Users");
    public static readonly SchemaMetadata IdentityUserClaim = new(Schema, "UserClaims");
    public static readonly SchemaMetadata IdentityUserLogin = new(Schema, "UserLogin");
    public static readonly SchemaMetadata IdentityUserRole = new(Schema, "UserRoles");
    public static readonly SchemaMetadata IdentityUserToken = new(Schema, "UserTokens");
    public static readonly SchemaMetadata IdentityRole = new(Schema, "Roles");
    public static readonly SchemaMetadata IdentityRoleClaim = new(Schema, "RoleClaims");
}
