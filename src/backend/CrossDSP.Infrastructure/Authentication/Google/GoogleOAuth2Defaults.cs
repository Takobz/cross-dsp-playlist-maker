namespace CrossDSP.Infrastructure.Authentication.Google
{
    public static class GoogleOAuth2Defaults
    {
        public const string GoogleOAuth2AuthenticationScheme = "GoogleOAuth2AuthenticationScheme";

        public const string GoogleOAuth2AccessTokenClaim = "GoogleOAuth2AccessToken";

        #region OAuth2.O APIs Constants

        public const string YouTubeScope = "https://www.googleapis.com/auth/youtube";
        public const string OfflineAccessType = "offline";
        public const string CodeResponseType = "code";
        public const string SelectAccountPrompt = "select_account";
        public const string AuthorizationCodeGrantType = "authorization_code";
        #endregion

    }
}