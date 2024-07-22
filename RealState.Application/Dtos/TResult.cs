using FluentValidation.Results;

namespace RealState.Application.Dtos
{
    public class TResult<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public List<ValidationFailure> Errors { get; }

        private TResult(bool isSuccess, T value, List<ValidationFailure> errors)
        {
            IsSuccess = isSuccess;
            Value = value;
            Errors = errors ?? [];
        }

        public static TResult<T> Success(T value) => new(true, value, null!);
        public static TResult<T> Failure(List<ValidationFailure> errors) => new(false, default!, errors);
    }
}
