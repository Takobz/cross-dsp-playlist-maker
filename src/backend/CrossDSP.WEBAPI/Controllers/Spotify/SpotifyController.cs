using CrossDSP.Infrastructure.Authentication.Spotify;
using CrossDSP.Infrastructure.Services.Spotify;
using CrossDSP.WEBAPI.DTOs.Responses;
using CrossDSP.WEBAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossDSP.WEBAPI.Controllers.Spotify
{
    [ApiController]
    [Route("[controller]")]
    public class SpotifyController : ControllerBase
    {
        private readonly ISpotifySearchService _spotifySearch;

        public SpotifyController(ISpotifySearchService spotifySearch)
        {
            _spotifySearch = spotifySearch;
        }

        [HttpGet]
        [Route("search/song")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchSongByName(
            [FromQuery(Name = "song_name")] string songName,
            [FromQuery(Name = "artist_name")] string? artistName
        )
        {
            var result = await _spotifySearch.SearchTrackByName(songName, artistName);
            var responses = result.ToSongSearchResponses();
            return Ok(new BaseResponse<SongSearchResponse>(responses));
        }

        [HttpGet]
        [Route("user/playlists")]
        [Authorize(
            AuthenticationSchemes = SpotifyOAuthDefaults.AuthenticationScheme,
            Policy = SpotifyOAuthDefaults.SpotifyUserPolicy)
        ]
        public async Task<IActionResult> GetUserPlaylists()
        {
            return Ok();
        }
    }
}