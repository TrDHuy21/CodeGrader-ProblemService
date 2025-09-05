using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ProblemService.Application.Validations
{
    public class ProblemValidation
    {
        public static ErrorField ValidName(string name)
        {
            ErrorField ErrorName = new ErrorField("Name", "");
            if (name == null || name.Length ==0 ) {
                ErrorName.ErrorMessage = "Name cannot be null";
                return ErrorName;
            }
            return ErrorName;
        }

        public static ErrorField ValidContent(string content)
        {
            ErrorField ErrorContent = new ErrorField("Content", "");
            if (content == null || content.Length == 0)
            {
                ErrorContent.ErrorMessage = "Content cannot be null";
                return ErrorContent;
            }
            return ErrorContent;
        }

        public static ErrorField ValidLevel(int level)
        {
            ErrorField ErrorLevel = new ErrorField("Level", "");
            if (level<=0 || level >3)
            {
                ErrorLevel.ErrorMessage = "Level must be 1: Easy , 2: Medium , 3: Hard";
                return ErrorLevel;
            }
            return ErrorLevel;
        }

        public static ErrorField ValidPromt(string promt)
        {
            ErrorField ErrorPromt = new ErrorField("Promt", "");
            if (promt == null || promt.Length == 0)
            {
                ErrorPromt.ErrorMessage = "Promt cannot be null";
                return ErrorPromt;
            }
            return ErrorPromt;
        }
    }
}
