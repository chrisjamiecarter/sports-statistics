namespace SportsStatistics.Infrastructure.Persistence.Schemas;

internal static class EntityFrameworkCoreSchema
{
    private const string Schema = "efcore";

    public static readonly SchemaMetadata MigrationsHistory = new(Schema, "Migrations");
}
