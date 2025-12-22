namespace CrossDSP.WEBAPI.DTOs.Responses
{
    public class InitiateAutorizeResponse : ResponseDTO
    {
        public InitiateAutorizeResponse(
            string redirectUri,
            string authorizationState
        )
        {
            AuthorizationCodeFlowRedirect = redirectUri;
            AuthorizationState = authorizationState;
        }

        public string AuthorizationCodeFlowRedirect { get; internal set; } = string.Empty;
        
        public string AuthorizationState { get; internal set; } = string.Empty;
    }
}