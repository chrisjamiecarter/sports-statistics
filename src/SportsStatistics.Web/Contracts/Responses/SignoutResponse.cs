namespace SportsStatistics.Web.Contracts.Requests;

internal sealed record SignoutResponse(bool IsSuccess,
                                       string Message);
