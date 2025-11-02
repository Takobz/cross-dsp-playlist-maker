using CrossDSP.Infrastructure.Services.Google.Models;
using CrossDSP.WEBAPI.DTOs.Responses;

namespace CrossDSP.WEBAPI.Mappers
{
    public static class GoogleResourcesToDTOs
    {
        /// <summary>
        /// Get Videos and present them as song objects.
        /// </summary>
        /// <param name="results">Search result from YouTube Data API</param>
        /// <returns></returns>
        public static IEnumerable<SongSearchResponse> ToSongSearchResponses(
            this YouTubeSearchResult results
        )
        {
            if (results.Items.Count == 0) return [];

            return results.Items.Where(item => item.Id.Kind == GoogleConstants.VideoResourceKind).Select(item =>
            {
                var mainArtistName = item.Snippet.ChannelTitle.ToUpper();
                if (mainArtistName.Contains(
                    "- TOPIC",
                    StringComparison.CurrentCultureIgnoreCase)
                )
                {
                    mainArtistName = mainArtistName.ToUpper().Replace("- TOPIC", "");
                }

                return new SongSearchResponse(
                    mainArtistName: mainArtistName,
                    dsp: GoogleConstants.YouTubeMusic,
                    dspSongId: item.Id.VideoId,
                    // at this point we should be sure the item is a video and hence has a title.
                    songTitle: item.Snippet.Title!
                );
            });
        }
    }
}