namespace CrossDSP.WEBAPI.DTOs.Responses
{
    public class DSPUserResponse : ResponseDTO
    {
        public DSPUserResponse(
            string dsp,
            string id,
            string username
        )
        {
            UserId = new DSPId(dsp, id);
            UserName = username;
        }

        public DSPId UserId { get; init; }
        public string UserName { get; init; }
    }
}