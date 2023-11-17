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
    internal class ExamConfig : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.Property(x => x.ExamScore).HasColumnType("decimal(18.2)");
            //builder.Property(x => x.ExamScore).HasPrecision(18,2);

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
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

            builder.HasIndex(x => x.AnswerId).IsUnique(false);

            builder.HasOne(x => x.Exam)
                .WithMany()
                .HasForeignKey(x => x.ExamId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }

    internal class TopicConfig : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Certificates)
                .WithMany(x => x.Topics)
                .UsingEntity<CertificateTopic>();
        }
    }


    internal class CertificateTopicConfig : IEntityTypeConfiguration<CertificateTopic>
    {
        public void Configure(EntityTypeBuilder<CertificateTopic> builder)
        {
            builder.Property(x => x.TopicPercentage).HasColumnType("decimal(18.2)");

            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasIndex(x => new { x.TopicId, x.CertificateId })
                .IsUnique();
        }
    }


    internal class CertificateConfig : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.Property(x => x.PassScore).HasColumnType("decimal(18.2)");

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }

    internal class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasQueryFilter(x => x.IsDeleted == false);

        }
    }

    internal class AnswerConfig : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasQueryFilter(x => x.IsDeleted == false);

        }
    }
}
