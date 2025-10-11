namespace CrossDSP.WEBAPI.DTOs.Responses
{
    public class InitiateAutorizeResponse : ResponseDTO
    {
        public InitiateAutorizeResponse(string redirectUri)
        {
            AuthorizationCodeFlowRedirect = redirectUri;
        }

        public string AuthorizationCodeFlowRedirect { get; internal set; } = string.Empty;
    }
}