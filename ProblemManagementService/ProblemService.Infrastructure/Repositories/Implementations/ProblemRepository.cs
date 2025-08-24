using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProblemService.Domain.Entities;
using ProblemService.Infrastructure.Context;
using ProblemService.Infrastructure.Repositories.Interfaces;

namespace ProblemService.Infrastructure.Repositories.Implementations
{
    public class ProblemRepository : GenericRepository<Problem>, IProblemRepository
    {
        public ProblemRepository(PMContext context) : base(context)
        {
        }

        public IQueryable<Problem> GetAll()
        {
            return _context.Problems.Include(p => p.InOutExamples).Include(p => p.ProblemTags).ThenInclude(pt => pt.Tag);
        }
    }
}
