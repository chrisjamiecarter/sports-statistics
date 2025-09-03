namespace SportsStatistics.Web.Contracts.Requests;

internal sealed record SignInResponse(bool IsSuccess,
                                      string Message);
