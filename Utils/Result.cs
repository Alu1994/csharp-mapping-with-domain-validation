using FluentValidation.Results;

namespace MappingWithDomainValidation.Utils
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public ValidationResult ValidationResult { get; }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public Result(ValidationResult validationResult)
        {
            IsSuccess = validationResult?.IsValid ?? true;
            ValidationResult = validationResult;
        }
    }

    public sealed class Result<TValue> : Result
    {
        public TValue Value { get; }

        public Result(TValue value) : base(true)
        {
            Value = value;
        }

        public Result(TValue value, ValidationResult validationResult) : base(validationResult)
        {
            Value = value;
        }
    }
}
