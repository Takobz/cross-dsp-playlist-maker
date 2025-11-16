using System.Text.RegularExpressions;

namespace CrossDSP.Infrastructure.Helpers
{
    public static class StringHelper
    {
        private const string Pattern = @"(?:\s*-\s*Topic|Vevo)\s*$";

        /// <summary>
        /// Builds a spotify query that we can send to spotify api.
        /// </summary>
        /// <param name="searchTerm">A term we use to search like a song name</param>
        /// <param name="searchFilters">filters  we can search with like artist, album, etc</param>
        /// <returns>A query that can be the original search term or search term with filters like: song_name artist:artist_name</returns>
        public static string BuildSpotifySearchQuery(
            this string searchTerm,
            Dictionary<string, string>? searchFilters = null
        )
        {
            var searchQuery = $"?q={searchTerm}";
            if (searchFilters == null) return $"{searchQuery}&type=track";

            foreach (var filterKey in searchFilters.Keys)
            {
                searchQuery += $" {filterKey}:{searchFilters[filterKey]}";
            }

            //we will have something like: hotline bing artist:drake&type=track
            return $"{searchQuery}&type=track";
        }

        /// <summary>
        /// YouTube usually appends music channels with - Topic or VEVO this checks for that
        /// </summary>
        public static bool HasTopicOrVevoSuffix(this string? input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            return Regex.IsMatch(input, Pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        }

        /// <summary>
        /// YouTube usually appends music channels with - Topic or VEVO this removes that for presentation
        /// </summary>
        public static string RemoveTopicOrVevoSuffix(this string? input)
        {
            if (string.IsNullOrEmpty(input)) return input ?? string.Empty;
            var result = Regex.Replace(input, Pattern, "", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            return result.TrimEnd();
        }

        /// <summary>
        ///  Turns id into spotify uri format
        /// </summary>
        /// <param name="items">spotify track ids</param>
        /// <returns>list of spotify track uri for each id</returns>
        public static IEnumerable<string> ToSpotifyTrackUris(
            this IEnumerable<string> items
        )
        {
            if (!items.Any()) return [];

            return items.Select(trackId => $"spotify:track:{trackId}");
        }
    }
}