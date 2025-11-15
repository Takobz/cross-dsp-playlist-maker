namespace CrossDSP.WEBAPI.DTOs.Responses
{
    public class PlaylistResponse : ResponseDTO
    {
        public PlaylistResponse(
            string dsp,
            string id,
            string name,
            string? description
        )
        {
            PlaylistId = new PlaylistId(dsp, id);
            PlaylistName = name;
            PlaylistDiscription = description;
        }

        public PlaylistId PlaylistId { get; init; } 
        public string PlaylistName { get; init; }
        public string? PlaylistDiscription { get; init; }
    }

    public class PlaylistId
    {
        public PlaylistId(string dsp, string id)
        {
            DSP = dsp;
            Id = id;
        }

        public string DSP { get; init; }
        public string Id { get; init; }
    }
}