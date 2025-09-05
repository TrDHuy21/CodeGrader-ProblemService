using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ProblemService.Application.Validations
{
    public class InOutExampleValidation
    {
        public static ErrorField ValidInput(string input)
        {
            ErrorField ErrorInput = new ErrorField("InputExample", "");
            if (input == null || input.Length == 0)
            {
                ErrorInput.ErrorMessage = "Input Example cannot be null";
                return ErrorInput;
            }
            return ErrorInput;
        }

        public static ErrorField ValidOutput(string output)
        {
            ErrorField ErrorOutput = new ErrorField("OutputExample", "");
            if (output == null || output.Length == 0)
            {
                ErrorOutput.ErrorMessage = "Output Example cannot be null";
                return ErrorOutput;
            }
            return ErrorOutput;
        }


        public static ErrorField ValidExplanation(string explain)
        {
            ErrorField ErrorExplanation = new ErrorField("Explanation", "");
            if (explain == null || explain.Length == 0)
            {
                ErrorExplanation.ErrorMessage = "Explanation cannot be null";
                return ErrorExplanation;
            }
            return ErrorExplanation;
        }
    }
}
