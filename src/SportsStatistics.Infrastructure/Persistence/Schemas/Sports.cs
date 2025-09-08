namespace SportsStatistics.Infrastructure.Persistence.Schemas;

internal static class Sports
{
    private const string Schema = "sports";

    public static readonly Metadata Players = new(Schema, "Players");
}
