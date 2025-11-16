namespace CrossDSP.WEBAPI.DTOs.Responses
{
    public class SongSearchResponse : ResponseDTO
    {
        public SongSearchResponse(
            string mainArtistName,
            string dsp,
            string dspSongId,
            string songTitle
        ) 
        {
            MainArtistName = mainArtistName;
            SongTitle = songTitle;
            SongId = new DSPId(dsp, dspSongId);
        }

        public string MainArtistName { get; internal set; }
        public string SongTitle { get; internal set; }
        public DSPId SongId { get; internal set; }
    }
}