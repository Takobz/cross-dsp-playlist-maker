namespace CrossDSP.WEBAPI.Options
{
    public class CorsOptions
    {
        public const string SectionName = "CorsOptions";

        public string[] AllowedOrigins { get; set; } = [];
    }
}