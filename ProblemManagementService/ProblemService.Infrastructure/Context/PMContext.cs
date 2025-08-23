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


            // === Seed Problem ===
            modelBuilder.Entity<Problem>().HasData(
                new Problem { Id = 1, Name = "Sum", Content = "Sum two numbers", Level = 1, Promt = "Sum prompt", IsDelete = false },
                new Problem { Id = 2, Name = "Multiply", Content = "Multiply two numbers", Level = 2, Promt = "Multiply prompt", IsDelete = false },
                new Problem { Id = 3, Name = "Palindrome", Content = "Check palindrome", Level = 3, Promt = "Palindrome prompt", IsDelete = false },
                new Problem { Id = 4, Name = "Sort", Content = "Sort a list", Level = 2, Promt = "Sort prompt", IsDelete = false },
                new Problem { Id = 5, Name = "Max", Content = "Find max value", Level = 1, Promt = "Max prompt", IsDelete = false }
            );

            // === Seed InOutExample ===
            modelBuilder.Entity<InOutExample>().HasData(
                new InOutExample { Id = 1, InputExample = "1 2", OutputExample = "3", Explanation = "1+2=3", ProblemId = 1, IsDelete = false },
                new InOutExample { Id = 2, InputExample = "2 3", OutputExample = "6", Explanation = "2*3=6", ProblemId = 2, IsDelete = false },
                new InOutExample { Id = 3, InputExample = "madam", OutputExample = "Yes", Explanation = "Palindrome", ProblemId = 3, IsDelete = false },
                new InOutExample { Id = 4, InputExample = "3 1 2", OutputExample = "1 2 3", Explanation = "Sorted", ProblemId = 4, IsDelete = false },
                new InOutExample { Id = 5, InputExample = "7 3 9", OutputExample = "9", Explanation = "Max is 9", ProblemId = 5, IsDelete = false }
            );

            // === Seed Tag ===
            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "Math", IsDelete = false },
                new Tag { Id = 2, Name = "String", IsDelete = false },
                new Tag { Id = 3, Name = "Array", IsDelete = false },
                new Tag { Id = 4, Name = "Beginner", IsDelete = false },
                new Tag { Id = 5, Name = "Intermediate", IsDelete = false }
            );

            // === Seed ProblemTag ===
            modelBuilder.Entity<ProblemTag>().HasData(
                new ProblemTag { ProblemId = 1, TagId = 1, IsDelete = false },
                new ProblemTag { ProblemId = 2, TagId = 1, IsDelete = false },
                new ProblemTag { ProblemId = 3, TagId = 2, IsDelete = false },
                new ProblemTag { ProblemId = 4, TagId = 3, IsDelete = false },
                new ProblemTag { ProblemId = 5, TagId = 1, IsDelete = false }
            );

        }
    }
}
