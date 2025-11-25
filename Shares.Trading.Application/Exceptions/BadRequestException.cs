using System.Net;

namespace Shares.Trading.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public static HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public int ErrorCode { get; private set; }
        public string Details { get; private set; }

        public BadRequestException(string title, string details, int errorCode) : base(title)
        {
            ErrorCode = errorCode;
            Details = details;
        }

        public BadRequestException(string message) : base(message)
        {
        }
    }
}
