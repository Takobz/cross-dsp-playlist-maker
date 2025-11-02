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
            SongId = new SongId(dsp, dspSongId);
        }

        public string MainArtistName { get; internal set; }
        public string SongTitle { get; internal set; }
        public SongId SongId { get; internal set; }
    }

    public class SongId
    {
        public SongId(string dsp, string dspSongId)
        {
            DSP = dsp;
            DSPSongId = dspSongId;
        }

        public string DSP { get; internal set; }
        public string DSPSongId { get; internal set; } 
    }
}