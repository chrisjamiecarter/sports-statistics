namespace SportsStatistics.Infrastructure.Persistence.Schemas;

internal static class SportsSchema
{
    private const string Schema = "sports";

    public static readonly SchemaMetadata Players = new(Schema, "Players");
}
