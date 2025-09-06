namespace SportsStatistics.Web.Contracts.Requests;

internal sealed record SigninResponse(bool IsSuccess,
                                      string Message);
