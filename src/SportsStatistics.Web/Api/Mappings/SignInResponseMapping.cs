using SportsStatistics.SharedKernel;
using SportsStatistics.Web.Contracts.Requests;

namespace SportsStatistics.Web.Api.Mappings;

internal static class SigninResponseMapping
{
    public static SigninResponse ToResponse(this Result result)
        => new(result.IsSuccess, result.Error.Description);
}
