using CrossDSP.Domain.Enums;

namespace CrossDSP.Domain.Exceptions
{
    public class AccessException : Exception
    {
        public AccessException(
            ExceptionCode exceptionCode,
            string message
        ) : base(message)
        {
            AccessExceptionCode = exceptionCode;
        }

        public ExceptionCode AccessExceptionCode { get; internal set; }
    }
}