namespace CrossDSP.WEBAPI.DTOs.Responses
{
    public class AuthorizeInitResponse : ResponseDTO
    {
        public AuthorizeInitResponse(
            string redirectUri,
            string authorizationState
        )
        {
            RedirectUri = redirectUri;
            AuthorizationState = authorizationState;
        }

        public string RedirectUri { get; internal set; } = string.Empty;

        public string AuthorizationState { get; internal set; } = string.Empty; 
    }
}