namespace SportsStatistics.Tools.DatabaseSeeder.Configuration;

public class SeederOptions
{
    public string DefaultPassword { get; set; } = "Password123!";

    public AdminUserOptions Admin { get; set; } = new();
    public ReportsUserOptions Reports { get; set; } = new();
    public TrackerUserOptions Tracker { get; set; } = new();

    public bool SeedTestData { get; set; } = true;
    public int YearsOfData { get; set; } = 3;
}

public sealed class AdminUserOptions
{
    public string Username { get; set; } = "admin";
    public string Email { get; set; } = "admin@sportsstatistics.com";
    public string Role { get; set; } = "Administrator";
}

public sealed class ReportsUserOptions
{
    public string Username { get; set; } = "reports";
    public string Email { get; set; } = "reports@sportsstatistics.com";
    public string Role { get; set; } = "ReportsViewer";
}

public sealed class TrackerUserOptions
{
    public string Username { get; set; } = "tracker";
    public string Email { get; set; } = "tracker@sportsstatistics.com";
    public string Role { get; set; } = "MatchTracker";
}
