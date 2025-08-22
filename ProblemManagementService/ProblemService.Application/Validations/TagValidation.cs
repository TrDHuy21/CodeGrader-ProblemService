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
            ErrorField ErrorName = new ErrorField("Name", new List<string>());
            if (name == null || name.Length == 0)
            {
                ErrorName.Message.Add("Name cannot be null");
            }
            return ErrorName;
        }
    }
}
