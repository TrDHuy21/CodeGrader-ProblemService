using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemService.Domain.Entities
{
    public class Problem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int Level { get; set; }
        public string Promt { get; set; }
        public bool IsDelete { get; set; }

        public List<InOutExample> InOutExamples { get; set; }
        public List<ProblemTag> ProblemTags { get; set; }
    }
}
