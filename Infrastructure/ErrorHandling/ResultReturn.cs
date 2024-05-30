using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topy_like_asp_webapi.Infrastructure.ErrorHandling
{
    public class ResultReturn
    {


        protected ResultReturn(bool success, string error)
        {
            if (success && error != string.Empty ||
                !success && error == string.Empty
                )
                throw new InvalidOperationException();

            // if (!success && error == string.Empty)
            //     throw new InvalidOperationException();

            Success = success;
            Error = error;
        }

        public bool Success { get; }
        public string Error { get; }
        public bool IsFailure => !Success;

        public static ResultReturn Fail(string message)
        {
            return new ResultReturn(false, message);
        }

        public static ResultReturn Ok()
        {
            return new ResultReturn(true, string.Empty);
        }

        public static ResultReturn<T> Fail<T>(string message)
        {
            return new ResultReturn<T>(default, false, message);
        }

        public static ResultReturn<T> Ok<T>(T value)
        {
            return new ResultReturn<T>(value, true, string.Empty);
        }

    }


    public class ResultReturn<T> : ResultReturn
    {
        protected internal ResultReturn(T value, bool success, string error)
            : base(success, error)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}



