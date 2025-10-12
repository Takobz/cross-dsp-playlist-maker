namespace CrossDSP.Infrastructure.Authentication.Google
{
    public static class GoogleOAuth2Defaults
    {
        public const string GoogleOAuth2AuthenticationScheme = "GoogleOAuth2AuthenticationScheme";

        #region OAuth2.O APIs Constants

        public const string YouTubeScope = "https://www.googleapis.com/auth/youtube.force-ssl";
        public const string OnlineAccessType = "offline";
        public const string CodeResponseType = "code";
        public const string SelectAccountPrompt = "select_account";
        #endregion

    }
}