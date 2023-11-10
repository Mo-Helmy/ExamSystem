using ExamSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Data.Config
{
    internal class ExamQuestionConfig : IEntityTypeConfiguration<ExamQuestion>
    {
        public void Configure(EntityTypeBuilder<ExamQuestion> builder)
        {
            builder.HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Answer)
                .WithOne()
                .HasForeignKey<ExamQuestion>(x => x.AnswerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Exam)
                .WithMany()
                .HasForeignKey(x => x.ExamId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    internal class TopicConfig : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
