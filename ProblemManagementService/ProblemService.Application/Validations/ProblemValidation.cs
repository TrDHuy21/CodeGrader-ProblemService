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
            ErrorField ErrorName = new ErrorField("Name", new List<string>());
            if (name == null || name.Length ==0 ) {
                ErrorName.Message.Add("Name cannot be null");
            }
            return ErrorName;
        }

        public static ErrorField ValidContent(string content)
        {
            ErrorField ErrorContent = new ErrorField("Content", new List<string>());
            if (content == null || content.Length == 0)
            {
                ErrorContent.Message.Add("Content cannot be null");
            }
            return ErrorContent;
        }

        public static ErrorField ValidLevel(int level)
        {
            ErrorField ErrorLevel = new ErrorField("Level", new List<string>());
            if (level<=0 || level >3)
            {
                ErrorLevel.Message.Add("Level must be 1: Easy , 2: Medium , 3: Hard");
            }
            return ErrorLevel;
        }

        public static ErrorField ValidPromt(string promt)
        {
            ErrorField ErrorPromt = new ErrorField("Promt", new List<string>());
            if (promt == null || promt.Length == 0)
            {
                ErrorPromt.Message.Add("Promt cannot be null");
            }
            return ErrorPromt;
        }
    }
}
