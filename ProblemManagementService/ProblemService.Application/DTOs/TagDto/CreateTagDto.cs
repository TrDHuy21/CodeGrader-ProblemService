using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemService.Application.DTOs.TagDto
{
    public class CreateTagDto
    {
        public string Name { get; set; }
        public bool IsDelete { get; set; }
    }
}
