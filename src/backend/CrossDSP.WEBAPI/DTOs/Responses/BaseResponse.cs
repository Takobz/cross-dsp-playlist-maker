namespace CrossDSP.WEBAPI.DTOs.Responses
{
    public class BaseResponse<TDto> where TDto : ResponseDTO
    {
        public BaseResponse(TDto dtoData)
        {
            Data = dtoData;
        }

        public BaseResponse(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }

        public TDto? Data { get; internal set; } = default;
        public IEnumerable<string> ErrorMessages { get; internal set; } = [];
    }

    /// <summary>
    /// An empty abstract class to help identify ResponseDTOs incase we need to apply common logic on them.
    /// </summary>
    public abstract class ResponseDTO { }
}