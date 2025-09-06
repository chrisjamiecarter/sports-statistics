using SportsStatistics.Core.Results;
using SportsStatistics.Web.Contracts.Requests;

namespace SportsStatistics.Web.Api.Mappings;

internal static class SignInResponseMapping
{
    public static SignInResponse ToResponse(this Result result)
        => new(result.IsSuccess, result.Error.Message);
}
