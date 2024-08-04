
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using RealState.Application.Exceptions;
using RealState.Application.Extras;

namespace RealState.Api.Middleware
{
    public class ErrorMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AppErrorException appErrorException)
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
                context.Response.StatusCode = (int)httpError.Type;
                if (context.Response.StatusCode != 204)
                {
                    await context.Response.WriteAsJsonAsync(json);
                }
            }
            catch (Exception e)
            {
                var json = new
                {
                    errors = e.Message
                };
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(json);
            }
        }
    }
}