using FluentValidation;

using MediatR;

using RealState.Application.Dtos;

namespace RealState.Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResult<TResponse>>
        where TRequest : IRequest<TResult<TResponse>>
    {
        private readonly IEnumerable<IValidator<TRequest>>? _validator = validators;

        public async Task<TResult<TResponse>> Handle(TRequest request, RequestHandlerDelegate<TResult<TResponse>> next, CancellationToken cancellationToken)
        {
            if (_validator == null) return await next();

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(
                validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken))).ConfigureAwait(false);

            var failures = validationResults.SelectMany(r => r.Errors)
                .Where(f => f != null).ToList();

            return failures.Count == 0 ? await next() : TResult<TResponse>.Failure(failures);
        }
    }
}
