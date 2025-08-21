using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemService.Application.DTOs.ProblemDto
{
    public class CreateProblemDto
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int Level { get; set; }
        public string Promt { get; set; }
        public bool IsDelete { get; set; }
    }
}
