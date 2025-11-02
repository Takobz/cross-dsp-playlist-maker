namespace CrossDSP.Infrastructure.Services.Google.Models
{
    public class YouTubeSearchResult
    {
        public string Kind { get; set; } = string.Empty;
        public string Etag { get; set; } = string.Empty;
        public string NextPageToken { get; set; } = string.Empty;
        public string RegionCode { get; set; } = string.Empty;
        public YouTubeSearchPageInfo PageInfo { get; set; }  = new();
        public List<YouTubeSearchItem> Items { get; set; } = [];
    }

    public class YouTubeSearchPageInfo
    {
        public int TotalResults { get; set; }
        public int ResultsPerPage { get; set; }
    }

    public class YouTubeSearchItem
    {
        public string Kind { get; set; } = string.Empty;

        public string Etag { get; set; } = string.Empty;

        public SearchResultId Id { get; set; } = new();

        public SearchResultSnippet Snippet { get; set; } = new();
    }

    public class SearchResultId
    {
        public string Kind { get; set; } = string.Empty;

        public string VideoId { get; set; } = string.Empty;

        public string ChannelId { get; set; } = string.Empty;

        public string PlaylistId { get; set; } = string.Empty;
    }

    public class SearchResultSnippet
    {
        public DateTimeOffset? PublishedAt { get; set; }

        public string? ChannelId { get; set; } = string.Empty;

        public string? Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Dictionary<string, Thumbnail> Thumbnails { get; set; } = [];

        public string ChannelTitle { get; set; } = string.Empty;

        public string LiveBroadcastContent { get; set; } = string.Empty;
    }

    public class Thumbnail
    {
        public string Url { get; set; } = string.Empty;

        public int Width { get; set; }

        public int Height { get; set; }
    }
}