
using RealState.Application.Enums;

namespace RealState.Application.Extras.ResultObject
{
    public record AppError(ErrorType Type, string Message, string PropertyName = "")
    {
        public AppError On(string propertyName)
        {
            return new AppError(Type, Message, propertyName);
        }
    }

    public static class AppErrorExt
    {
        public static AppError Because(this ErrorType self, string msg)
        {
            return new(self, msg);
        }
    }

    public static class AppErrors
    {
    }
}