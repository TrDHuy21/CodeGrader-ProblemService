using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemService.Application.DTOs.InOutExampleDto;
using ProblemService.Application.DTOs.TagDto;

namespace ProblemService.Application.DTOs.ProblemDto
{
    public class ProblemDtoDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int Level { get; set; }
        public string Promt { get; set; }

        public List<TagDtoDetail> tags { get; set; }

        public List<InOutExampleDtoDetail> inOutExamples { get; set; }

        public bool IsDelete { get; set; }
    }
}
