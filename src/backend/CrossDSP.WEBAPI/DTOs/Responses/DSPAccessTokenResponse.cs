namespace CrossDSP.WEBAPI.DTOs.Responses
{
    public class DSPAccessTokenResponse : ResponseDTO
    {
        public DSPAccessTokenResponse(
            string accessToken,
            int expiresIn,
            string refreshToken
        )
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            RefreshToken = refreshToken;
        }
        
        public string AccessToken { get; internal set; }
    
        public int ExpiresIn { get; internal set; }

        public string RefreshToken { get; internal set; }
    }
}