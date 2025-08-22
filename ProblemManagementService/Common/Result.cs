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
        public T data { get; set; }
        public string ErrorMessage { get; set; }

        public List<ErrorField> detail { get; set; }

        public Result(bool success, T data, string errorMessage, List<ErrorField> detail)
        {
            this.isSuccess = success;
            this.data = data;
            ErrorMessage = errorMessage;
            this.detail = detail;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, null, new List<ErrorField>());
        }
        public static Result<T> Failure(string errorMessage , List<ErrorField> errorFields)
        {
            return new Result<T>(false, default(T), errorMessage,errorFields);
        }


    }
}
