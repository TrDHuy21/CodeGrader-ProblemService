using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ProblemService.Application.Validations
{
    public class TagValidation
    {
        public static ErrorField ValidName(string name)
        {
            ErrorField ErrorName = new ErrorField("Name", "");
            if (name == null || name.Length == 0)
            {
                ErrorName.ErrorMessage = "Name cannot be null";
                return ErrorName;
            }
            return ErrorName;
        }
    }
}
