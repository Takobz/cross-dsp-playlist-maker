namespace CrossDSP.WEBAPI.DTOs.Responses
{
    public class SuccessResponse : ResponseDTO
    {
        public SuccessResponse(bool isSuccess)
        {
            Success = isSuccess;  
        }

        public bool Success { get; init; }
    }
}