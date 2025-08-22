using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemService.Application.DTOs.ProblemTagDto
{
    public class CreateProblemTagDto
    {
        public int ProblemId { get; set; }
        public int TagId { get; set; }
        public bool IsDelete { get; set; }
    }
}
