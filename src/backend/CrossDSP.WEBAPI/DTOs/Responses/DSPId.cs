namespace CrossDSP.WEBAPI.DTOs.Responses
{
    public class DSPId
    {
        public DSPId(string dsp, string id)
        {
            DSP = dsp;
            Id = id;
        }

        public string DSP { get; init; }
        public string Id { get; init; }
    }
}