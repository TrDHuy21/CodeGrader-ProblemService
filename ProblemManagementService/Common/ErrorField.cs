using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ErrorField
    {
        public string Field {  get; set; }  
        public List<string> Message { get; set; }

        public ErrorField(string field, List<string> message)
        {
            Field = field;
            Message = message;
        }
    }
}
