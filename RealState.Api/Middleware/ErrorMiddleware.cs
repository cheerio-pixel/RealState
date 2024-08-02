using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using RealState.Application.Exceptions;
using RealState.Application.Extras;

namespace RealState.Api.Middleware
{
    public class ErrorMiddleware : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is AppErrorException appErrorException)
            {
                HttpError? httpError = appErrorException.InnerError.FirstOrDefault();
                if (httpError is default(HttpError))
                {
                    throw new InvalidDataException("HTTP ERROR null when is not expected.", appErrorException);
                }
                var json = new
                {
                    errors = appErrorException.InnerError
                };

                filterContext.Result = new ObjectResult(json) { StatusCode = (int)httpError.Type };
                filterContext.HttpContext.Response.StatusCode = (int)httpError.Type;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}