using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProblemService.Domain.Entities;
using ProblemService.Infrastructure.Configurations;

namespace ProblemService.Infrastructure.Context
{
    public class PMContext : DbContext
    {
        public PMContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Problem> Problems { get; set; }
        public DbSet<InOutExample> InOutExamples { get; set; }
        public DbSet<ProblemTag> ProblemTags { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new InOutExampleConfiguration());
            modelBuilder.ApplyConfiguration(new ProblemConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new ProblemTagConfiguration());

        }
    }
}
