using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProblemService.Domain.Entities;

namespace ProblemService.Infrastructure.Configurations
{
    public class InOutExampleConfiguration : IEntityTypeConfiguration<InOutExample>
    {
        public void Configure(EntityTypeBuilder<InOutExample> builder)
        {

            builder.ToTable(nameof(InOutExample));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(i => i.Problem)
                   .WithMany(p => p.InOutExamples)
                   .HasForeignKey(i => i.ProblemId);
        }
    }
}
