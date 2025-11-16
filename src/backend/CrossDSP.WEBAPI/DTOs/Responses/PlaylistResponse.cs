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
            PlaylistId = new DSPId(dsp, id);
            PlaylistName = name;
            PlaylistDiscription = description;
        }

        public DSPId PlaylistId { get; init; } 
        public string PlaylistName { get; init; }
        public string? PlaylistDiscription { get; init; }
    }
}