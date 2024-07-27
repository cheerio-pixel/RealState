using System.Net;

namespace RealState.Application.Extras
{
    public record HttpError(HttpStatusCode Type, string Message, string PropertyName = "")
    {
        public HttpError On(string propertyName)
        {
            return new HttpError(Type, Message, propertyName);
        }
    }

    public static class HttpErrorExt
    {
        public static HttpError Because(this HttpStatusCode self, string msg)
        {
            return new(self, msg);
        }
    }
}