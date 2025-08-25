using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using ProblemService.Infrastructure.Context;
using ProblemService.Infrastructure.Repositories.Implementations;
using ProblemService.Infrastructure.Repositories.Interfaces;

namespace ProblemService.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly PMContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(PMContext context)
        {
            _context = context;
            InOutExamples = new InOutExampleRepository(_context);
            Tags = new TagRepository(_context);
            Problems = new ProblemRepository(_context);
            ProblemTags = new ProblemTagRepository(_context);   

        }

        public IInOutExampleRepository InOutExamples { get; }

        public ITagRepository Tags { get; }

        public IProblemRepository Problems { get; }

        public IProblemTagRepository ProblemTags { get; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
