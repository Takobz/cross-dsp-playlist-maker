using CrossDSP.Infrastructure.Authentication.Google;
using CrossDSP.Infrastructure.Services.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossDSP.WEBAPI.Controllers.Google
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = GoogleOAuth2Defaults.GoogleOAuth2AuthenticationScheme)]
    public class GoogleController : ControllerBase
    {
        private readonly IYouTubeResourceService _ytResourceService;
        private readonly ILogger<GoogleController> _logger;

        public GoogleController(
            IYouTubeResourceService ytResourceService,
            ILogger<GoogleController> logger
        )
        {
            _ytResourceService = ytResourceService;
            _logger = logger;
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Policy = GoogleOAuth2Defaults.HasGoogleAccessTokenPolicy)]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            await _ytResourceService.SearchYouTube(query);

            return Ok();
        }
    }
}