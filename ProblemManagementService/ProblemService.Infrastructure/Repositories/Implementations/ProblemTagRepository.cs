using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemService.Domain.Entities;
using ProblemService.Infrastructure.Context;
using ProblemService.Infrastructure.Repositories.Interfaces;

namespace ProblemService.Infrastructure.Repositories.Implementations
{
    public class ProblemTagRepository : GenericRepository<ProblemTag>, IProblemTagRepository
    {
        public ProblemTagRepository(PMContext context) : base(context)
        {
        }
    }
}
