using System.Net;

using FluentValidation;

using MediatR;

using RealState.Application.Exceptions;
using RealState.Application.Extras;

namespace RealState.Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>>? _validator = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator == null) return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                _validator.Select(v =>
                    v.ValidateAsync(context, cancellationToken))).ConfigureAwait(false);

            var failures = validationResults.SelectMany(r => r.Errors)
                .Where(f => f != null).ToList();
            if (failures.Count != 0)
            {
                throw new AppErrorException(failures.Select(f => new HttpError(
                    HttpStatusCode.BadRequest,
                    f.ErrorMessage,
                    f.PropertyName
                )));
            }
            return await next();
        }
    }
}
