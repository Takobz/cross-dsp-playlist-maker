using CrossDSP.Infrastructure.Authentication.Spotify;
using CrossDSP.Infrastructure.Authentication.Spotify.Services;
using CrossDSP.WEBAPI.DTOs.Responses;
using CrossDSP.WEBAPI.Services.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossDSP.WEBAPI.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IGoogleAuthService _googleAuthService;
        private readonly ISpotifyOAuthProvider _spotifyAuthProvider;

        public AuthController(
            ILogger<AuthController> logger,
            IGoogleAuthService googleAuthService,
            ISpotifyOAuthProvider spotifyOAuthProvider
        )
        {
            _logger = logger;
            _googleAuthService = googleAuthService;
            _spotifyAuthProvider = spotifyOAuthProvider;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("google-init")]
        [ProducesResponseType(typeof(BaseResponse<InitiateAutorizeResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<InitiateAutorizeResponse>>> InitiateGoogleAuthorize()
        {
            var result = await _googleAuthService.GetGoogleAuthorizeUrl();
            return Ok(new BaseResponse<InitiateAutorizeResponse>(
                new InitiateAutorizeResponse(
                    result.RedirectUri,
                    result.AuthorizationState
                )
            ));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("google-callback")]
        [ProducesDefaultResponseType(typeof(BaseResponse<DSPAccessTokenResponse>))]
        public async Task<ActionResult> GoogleCallBack(
            [FromQuery] string code,
            [FromQuery] string state,
            [FromQuery] string scope
        )
        {
            //TODO validate state - get from Cache??

            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
            {
                return BadRequest(); //TODO: refine this....
            }

            await _googleAuthService.GetAccessToken(code, state);
            return Ok("<h1> Close This Window <h1/>"); //return a view :)
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("google-token")]
        [ProducesResponseType(typeof(BaseResponse<DSPAccessTokenResponse?>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<DSPAccessTokenResponse?>>> GetGoogleAccessToken(
            [FromQuery(Name = "authorization_state")] string authorizationState
        )
        {
            var result = await _googleAuthService.TryGetAccessTokenFromCache(
                authorizationState
            );

            return Ok(new BaseResponse<DSPAccessTokenResponse>(
                dtoData: result!
            ));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("spotify-init")]
        [ProducesResponseType(typeof(BaseResponse<InitiateAutorizeResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<InitiateAutorizeResponse>>> InitiateSpotifyAuthorize()
        {
            var scopes = SpotifyOAuthDefaults.UserPublicPlaylistSpotifyScopes();
            var result = await _spotifyAuthProvider.InitiateAuthorizationRequest(
                scopes
            );

            return Ok(new BaseResponse<InitiateAutorizeResponse>(
                new InitiateAutorizeResponse(
                    result.AuthorizeRedirectUrl,
                    result.AuthorizationState
                )
            ));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("spotify-callback")]
        [ProducesDefaultResponseType(typeof(BaseResponse<DSPAccessTokenResponse>))]
        public async Task<ActionResult> SpotifyCallback(
            [FromQuery] string code,
            [FromQuery] string state
        )
        {
            //TODO validate state - get from Cache??

            if (string.IsNullOrEmpty(code))
            {
                return BadRequest(); //TODO: refine this....
            }

            var accessToken = await _spotifyAuthProvider.GetUserAccessToken(code);
            return Ok(new BaseResponse<DSPAccessTokenResponse>(
                new DSPAccessTokenResponse(
                    accessToken.AccessToken,
                    accessToken.ExpiresIn,
                    accessToken.RefreshToken
                )
            ));
        }
    }
}