﻿namespace SportsStatistics.Infrastructure.Database;

internal static class Schemas
{
    internal sealed record SchemaMetadata(string Schema, string Table);

    private const string Schema = "sports";

    public static readonly SchemaMetadata Competitions = new(Schema, "Competitions");
    public static readonly SchemaMetadata Fixtures = new(Schema, "Fixtures");
    public static readonly SchemaMetadata MigrationsHistory = new(Schema, "__EFMigrationsHistory");
    public static readonly SchemaMetadata Players = new(Schema, "Players");
    public static readonly SchemaMetadata Seasons = new(Schema, "Seasons");
}
