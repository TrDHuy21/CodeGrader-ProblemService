using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemService.Application.DTOs.TagDto
{
    public class TagDtoDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }
    }
}
