using System.Security.Claims;
using CrossDSP.Infrastructure.Authentication.Spotify;
using CrossDSP.Infrastructure.Helpers;
using CrossDSP.Infrastructure.Services.Spotify;
using CrossDSP.Infrastructure.Services.Spotify.Models;
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
        private readonly ISpotifyPlaylistService _spotifyPlaylistService;

        public SpotifyController(
            ISpotifySearchService spotifySearch,
            ISpotifyPlaylistService spotifyPlaylistService
        )
        {
            _spotifySearch = spotifySearch;
            _spotifyPlaylistService = spotifyPlaylistService;
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
        [Route("user")]
        [Authorize(
            AuthenticationSchemes = SpotifyOAuthDefaults.AuthenticationScheme,
            Policy = SpotifyOAuthDefaults.SpotifyUserPolicy
        )]
        public async Task<IActionResult> GetUser()
        {
            var spotifyUserId = HttpContext.User.GetClaimValueByName(
                SpotifyOAuthDefaults.SpotifyUserEntityIdClaimKey
            );

            var spotifyUserName = HttpContext.User.GetClaimValueByName(
                ClaimTypes.Name
            );

            return Ok(new BaseResponse<DSPUserResponse>(
                new DSPUserResponse(
                    dsp: SpotifyConstants.Spotify,
                    id: spotifyUserId,
                    username: spotifyUserName
                )
            ));
        }

        [HttpGet]
        [Route("user/{userId}/playlists")]
        [Authorize(
            AuthenticationSchemes = SpotifyOAuthDefaults.AuthenticationScheme,
            Policy = SpotifyOAuthDefaults.SpotifyUserPolicy)
        ]
        public async Task<IActionResult> GetUserPlaylists(
            string userId
        )
        {
            var result = await _spotifyPlaylistService.GetUserPlaylists(userId);
            if (result.HasData)
            {
               var responses = result.Data.ToPlaylistResponses();
               return Ok(new BaseResponse<PlaylistResponse>(responses)); 
            }

            return Ok(new BaseResponse<PlaylistResponse>(Enumerable.Empty<PlaylistResponse>()));
        }

        [HttpGet]
        [Route("user/{userId}/playlists/{playlistId}")]
        [Authorize(
            AuthenticationSchemes = SpotifyOAuthDefaults.AuthenticationScheme,
            Policy = SpotifyOAuthDefaults.SpotifyUserPolicy)
        ]
        public async Task<IActionResult> AddTracksToPlaylist(
            string userId,
            string playlistId
        )
        {
            return Ok();
        }
    }
}