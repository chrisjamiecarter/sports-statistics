namespace SportsStatistics.Web.Api.Endpoints;

internal static class Routes
{
    private const string ApiBase = "/api";

    public static class Identity
    {
        private const string IdentityBase = $"{ApiBase}/identity";

        public const string SignIn = $"{IdentityBase}/signin";
        public const string SignOut = $"{IdentityBase}/signout";
    }
}
