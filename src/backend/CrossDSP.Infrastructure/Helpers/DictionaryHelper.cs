using Microsoft.AspNetCore.Http;

namespace CrossDSP.Infrastructure.Helpers
{
    public static class DictionaryHelper
    {
        public static string GenerateQueryParameters(this Dictionary<string, string> queryParams)
        {
            var fullQueryParams = "?";
            foreach (var key in queryParams.Keys)
            {
                fullQueryParams += $"{key}={queryParams[key]}&";
            }

            //remove last & in query param
            return fullQueryParams.Substring(0, fullQueryParams.Length - 1);
        }

        public static string ExtractBearerTokenFromHeader(this IHeaderDictionary requestHeader) 
        {
            if (requestHeader.TryGetValue("Authorization", out var accessTokenWithBearerPrefixStringValue) &&
                !string.IsNullOrEmpty(accessTokenWithBearerPrefixStringValue)
            )
            {
                //remove prefix "Bearer "
                var accessTokenWithBearerPrefix = accessTokenWithBearerPrefixStringValue.First();
                return accessTokenWithBearerPrefix![7..];
            }

            return string.Empty;
        }
    }
}