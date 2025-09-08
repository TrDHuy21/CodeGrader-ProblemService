using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Result<T>
    {
        public bool isSuccess { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        public ErrorDetail errorDetail { get; set; }

        public Result(bool success, T data, string message, ErrorDetail detail)
        {
            this.isSuccess = success;
            this.Data = data;
            this.Message = message;
            this.errorDetail = detail;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, null, null);
        }
        public static Result<T> Failure(string errorMessage , ErrorDetail errorFields)
        {
            return new Result<T>(false, default(T), errorMessage,errorFields);
        }


    }
}
