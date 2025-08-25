using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemService.Domain.Entities;

namespace ProblemService.Infrastructure.Repositories.Interfaces
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        IQueryable<Tag> GetAll();
    }
}
