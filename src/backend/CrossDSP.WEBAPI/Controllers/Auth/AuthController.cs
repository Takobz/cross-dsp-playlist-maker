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

        public AuthController(
            ILogger<AuthController> logger,
            IGoogleAuthService googleAuthService
        )
        {
            _logger = logger;
            _googleAuthService = googleAuthService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("google-init")]
        [ProducesResponseType(typeof(BaseResponse<InitiateAutorizeResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<InitiateAutorizeResponse>>> InitiateGoogleAuthorize()
        {
            var redirectUrl = await _googleAuthService.GetGoogleAuthorizeUrl();
            return Ok(new BaseResponse<InitiateAutorizeResponse>(
                new InitiateAutorizeResponse(redirectUrl)
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

            if (string.IsNullOrEmpty(code))
            {
                return BadRequest(); //TODO: refine this....
            }

            var accessToken = await _googleAuthService.GetAccessToken(code);
            return Ok(new BaseResponse<DSPAccessTokenResponse>(
                accessToken
            ));
        }
    }
}