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
    public class ProblemTagConfiguration : IEntityTypeConfiguration<ProblemTag>
    {
        public void Configure(EntityTypeBuilder<ProblemTag> builder)
        {
            builder.ToTable(nameof(ProblemTag));
            builder.HasKey(x => x.ProblemId);
            builder.Property(x => x.ProblemId).ValueGeneratedOnAdd();
            builder.HasOne(pt => pt.Problem)
                   .WithMany(p => p.ProblemTags)
                   .HasForeignKey(pt => pt.ProblemId);
            builder.HasOne(pt => pt.Tag)
                   .WithMany(t => t.ProblemTags)
                   .HasForeignKey(pt => pt.TagId);



        }
    }
}
