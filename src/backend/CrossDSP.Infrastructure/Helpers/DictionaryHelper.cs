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
    }
}