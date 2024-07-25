using Microsoft.AspNetCore.WebUtilities;

using RealState.Application.Interfaces.Services;

namespace RealState.Infrastructure.Shared.Services
{
    public class UriServices(string origin) : IUriServices
    {
        private readonly string _origin = origin;

        public string GetUrl(string token, string userId, string path)
        {
            var route = $"/Account/{path}";

            var uri = new Uri(string.Concat(_origin, route));
            var finalUrl = QueryHelpers.AddQueryString(uri.ToString(), "Token", token);
            finalUrl = QueryHelpers.AddQueryString(finalUrl, "UserId", userId);

            return finalUrl;
        }
    }
}
