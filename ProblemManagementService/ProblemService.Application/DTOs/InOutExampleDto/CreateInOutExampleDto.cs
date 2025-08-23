using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemService.Application.DTOs.InOutExampleDto
{
    public class CreateInOutExampleDto
    {
        public string InputExample { get; set; }
        public string OutputExample { get; set; }
        public string Explanation { get; set; }

        public int ProblemId { get; set; }
        public bool IsDelete { get; set; }
    }
}
