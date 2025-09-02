namespace SportsStatistics.Web.Contracts.Requests;

internal sealed record SignInRequest(string? Email = null,
                                     string? Password = null,
                                     bool IsPersistant = false,
                                     string? ReturnUrl = null);
