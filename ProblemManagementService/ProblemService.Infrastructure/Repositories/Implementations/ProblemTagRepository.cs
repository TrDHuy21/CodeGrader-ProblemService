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
    public class ProblemTagRepository : GenericRepository<ProblemTag>, IProblemTagRepository
    {
        public ProblemTagRepository(PMContext context) : base(context)
        {
        }

        public IQueryable<ProblemTag> GetAllInclude()
        {
            return _context.ProblemTags
                   .Include(pt => pt.Problem)
                   .Include(pt => pt.Tag);
        }


        public ProblemTag? GetProblemTagById(int ProblemId, int TagId)
        {
             return _context.ProblemTags
                    .Include(pt => pt.Problem)
                    .Include(pt => pt.Tag)
                    .FirstOrDefault(pt => pt.ProblemId == ProblemId && pt.TagId == TagId);

        }
    }
}
