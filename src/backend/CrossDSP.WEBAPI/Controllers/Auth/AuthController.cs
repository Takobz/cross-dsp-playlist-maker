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
    }
}