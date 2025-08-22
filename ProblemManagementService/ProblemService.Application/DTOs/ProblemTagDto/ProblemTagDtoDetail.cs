using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemService.Domain.Entities;

namespace ProblemService.Application.DTOs.ProblemTagDto
{
    public class ProblemTagDtoDetail
    {
        public int ProblemId { get; set; }

        public string ProblemName { get; set; }
        public int TagId { get; set; }

        public string TagName { get; set; }
        public bool IsDelete { get; set; }
    }
}
