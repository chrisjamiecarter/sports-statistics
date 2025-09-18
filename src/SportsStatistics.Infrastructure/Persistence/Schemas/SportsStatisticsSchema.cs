namespace SportsStatistics.Infrastructure.Persistence.Schemas;

internal static class SportsStatisticsSchema
{
    internal sealed record SchemaMetadata(string Schema, string Table);

    private const string Schema = "sports";

    public static readonly SchemaMetadata MigrationsHistory = new(Schema, "__EFMigrationsHistory");
    public static readonly SchemaMetadata Players = new(Schema, "Players");
}
