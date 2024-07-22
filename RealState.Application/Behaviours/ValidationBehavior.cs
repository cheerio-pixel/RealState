using FluentValidation;

using MediatR;

using RealState.Application.Dtos;

namespace RealState.Application.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator) : IPipelineBehavior<TRequest, TResult<TResponse>>
        where TRequest : IRequest<TResult<TResponse>>
    {
        private readonly IValidator<TRequest>? _validator = validator;

        public async Task<TResult<TResponse>> Handle(TRequest request, RequestHandlerDelegate<TResult<TResponse>> next, CancellationToken cancellationToken)
        {
            if (_validator == null) return await next();

            var validatorResult = await _validator!.ValidateAsync(request, cancellationToken);
            return validatorResult.IsValid ? await next() : TResult<TResponse>.Failure([.. validatorResult.Errors]);
        }
    }
}
