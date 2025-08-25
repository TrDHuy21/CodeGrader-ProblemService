using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using ProblemService.Infrastructure.Repositories.Interfaces;

namespace ProblemService.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IInOutExampleRepository InOutExamples { get; }
        ITagRepository Tags { get; }
        IProblemRepository Problems { get; }
        IProblemTagRepository ProblemTags { get; }


        // transaction 
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveChangeAsync();



    }
}
