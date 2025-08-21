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

        public Result(bool success, T data, string errorMessage)
        {
            this.isSuccess = success;
            this.data = data;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true,value,null);
        }
        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>(false, default(T), errorMessage);
        }


    }
}
