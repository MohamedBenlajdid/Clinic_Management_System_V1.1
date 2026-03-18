using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic_Management_BLL.ResultWraper
{
    public class Result
    {
        private readonly List<string> _errors;

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public string? SuccessMessage { get; }
        public string? ErrorMessage =>
    _errors.Count > 0 ? _errors[0] : null;

        public IReadOnlyList<string> Errors => _errors;

        protected Result(
            bool success,
            string? successMessage = null,
            IEnumerable<string>? errors = null)
        {
            IsSuccess = success;
            SuccessMessage = success ? successMessage : null;
            _errors = errors?.ToList() ?? new List<string>();
        }

        // ✅ Success
        public static Result Ok(string? message = null)
            => new Result(true, message);

        // ✅ Single error
        public static Result Fail(string error)
            => new Result(false, null, new[] { error });

        //  Multiple errors
        public static Result Fail(IEnumerable<string> errors)
            => new Result(false, null, errors);
    }





    public sealed class Result<T> : Result
    {
        private readonly T _value;

        public T Value =>
            IsSuccess
                ? _value
                : throw new InvalidOperationException(
                    "Cannot access Value when result is failure.");

        private Result(T value, string? message = null)
            : base(true, message)
        {
            _value = value;
        }

        private Result(IEnumerable<string> errors)
            : base(false, null, errors)
        {
            _value = default!;
        }

        // ✅ Success with optional message
        public static Result<T> Ok(T value, string? message = null)
            => new Result<T>(value, message);

        public static new Result<T> Fail(string error)
            => new Result<T>(new[] { error });

        public static new Result<T> Fail(IEnumerable<string> errors)
            => new Result<T>(errors);
    }






}
