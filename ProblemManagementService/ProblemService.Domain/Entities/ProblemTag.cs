using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemService.Domain.Entities
{
    public class ProblemTag
    {
        public int ProblemId { get; set; }
        public int TagId { get; set; }
        public bool IsDelete { get; set; }

        public Tag Tag { get; set; }
        public Problem Problem { get; set; }

    }
}
