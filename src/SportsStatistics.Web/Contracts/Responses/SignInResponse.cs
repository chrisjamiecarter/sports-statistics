namespace SportsStatistics.Web.Contracts.Requests;

internal sealed record SignInResponse(bool IsSuccess = false,
                                      string? ReturnUrl = null,
                                      string? ErrorMessage = null);
