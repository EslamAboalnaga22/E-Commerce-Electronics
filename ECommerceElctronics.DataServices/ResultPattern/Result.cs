using System.Text.Json.Serialization;

namespace ECommerceElctronics.DataServices.ResultPattern
{
    public class Result<T>
    {
        private Result(T value)
        {
            Value = value;
            Error = null;
        }
        private Result(Error error)
        {
            Value = default;
            Error = error;
        }

        [JsonConstructor]
        public Result(T? value, Error? error)
        {
            Value = value; 
            Error = error;
        }

        public T? Value { get; }
        public Error? Error { get; }
        public bool IsSuccess => Error is null;

        public static Result<T> Success(T value) => new(value);
        public static Result<T> Failure(Error error) => new(error);

        public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Value!) : onFailure(Error!);
        }
    }
}
