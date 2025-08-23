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
            ErrorField ErrorInput = new ErrorField("InputExample", new List<string>());
            if (input == null || input.Length == 0)
            {
                ErrorInput.Message.Add("Input Example cannot be null");
            }
            return ErrorInput;
        }

        public static ErrorField ValidOutput(string output)
        {
            ErrorField ErrorOutput = new ErrorField("OutputExample", new List<string>());
            if (output == null || output.Length == 0)
            {
                ErrorOutput.Message.Add("Output Example cannot be null");
            }
            return ErrorOutput;
        }


        public static ErrorField ValidExplanation(string explain)
        {
            ErrorField ErrorExplanation = new ErrorField("Explanation", new List<string>());
            if (explain == null || explain.Length == 0)
            {
                ErrorExplanation.Message.Add("Explanation cannot be null");
            }
            return ErrorExplanation;
        }
    }
}
