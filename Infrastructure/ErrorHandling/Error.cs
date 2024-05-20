using Microsoft.AspNetCore.Mvc;

namespace PostApi.Infrastructure.ErrorHandling
{
    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error Unspecified = new("10101010", "An unspecified domain error occurred");
    }

    public class Result
    {
        private Result(bool isSuccess, Error error, object? data = null)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
            Data = data;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public object? Data { get; }

        public static Result Success() => new(true, Error.None);
        public static Result Success(object data) => new(true, Error.None, data);
        public static Result Failure() => new(false, Error.Unspecified);
        public static Result Failure(string message) => new(false, Error.Unspecified, message);
        public static Result Failure(Error error) => new(false, error);
        public static Result Failure(Error error, object? data) => new(false, error, data);
        public static ActionResult Handler(Result result)
        {
            if (result.IsSuccess)
            {
                if (result.Data != null)
                {
                    return new OkObjectResult(result.Data);
                }
                else
                {
                    return new NoContentResult();
                }
            }
            else
            {
                if (result.Data == null)
                {
                    return new BadRequestObjectResult(result.Error);
                }
                else if (result.Data != null)
                {
                    return new BadRequestObjectResult(MergeErrorAndData(result.Error, result.Data));
                }
            }
            throw new ArgumentException("Invalid error");
        }

        private static object MergeErrorAndData(Error error, object data)
        {
            return new { error, data };
        }
    }
}