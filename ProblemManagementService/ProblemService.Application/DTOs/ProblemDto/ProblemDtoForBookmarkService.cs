using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemService.Application.DTOs.InOutExampleDto;
using ProblemService.Application.DTOs.TagDto;

namespace ProblemService.Application.DTOs.ProblemDto
{
    public class ProblemDtoForBookmarkService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        public List<TagDtoDetail> tags { get; set; }

        public bool IsDelete { get; set; }
    }
}
