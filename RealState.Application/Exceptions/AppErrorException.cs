using System.Net;

using RealState.Application.Enums;
using RealState.Application.Extras;
using RealState.Application.Extras.ResultObject;

namespace RealState.Application.Exceptions
{
    public class AppErrorException
    : Exception
    {
        public IEnumerable<HttpError> InnerError { get; set; } = Enumerable.Empty<HttpError>();

        public AppErrorException(IEnumerable<HttpError> innerError)
        {
            InnerError = innerError;
        }

        public AppErrorException(HttpError innerError)
        {
            InnerError = InnerError.Append(innerError);
        }

        public AppErrorException() { }

        public AppErrorException(string? message)
        : base(message)
        {
            InnerError = InnerError.Append(HttpStatusCode.BadRequest.Because(message ?? ""));
        }

        public AppErrorException(string? message, Exception? innerException)
        : base(message, innerException)
        {
            InnerError = InnerError.Append(HttpStatusCode.BadRequest.Because(message ?? ""));
        }
    }

    public static class AppErrorExceptionExt
    {
        public static void Throw(this HttpError error)
        {
            throw new AppErrorException(error);
        }

        public static void Throw(this string error)
        {
            throw new AppErrorException(error);
        }
    }
}