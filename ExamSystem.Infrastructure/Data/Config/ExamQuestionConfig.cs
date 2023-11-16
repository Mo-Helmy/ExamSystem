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
            //builder.HasKey(x => new {x.CertificateId, x.TopicId});

            builder.HasQueryFilter(x => x.IsDeleted == false);

            builder.HasIndex(x => new { x.TopicId, x.CertificateId })
                .IsUnique();
        }
    }


    internal class CertificateConfig : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
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

    //internal class ExamQuestionStoredProcedureConfig : IEntityTypeConfiguration<ExamQuestionStoredProcedure>
    //{
    //    public void Configure(EntityTypeBuilder<ExamQuestionStoredProcedure> builder)
    //    {
    //        builder.HasNoKey();

    //    }
    //}
    //internal class ExamAnswersStoredProcedureConfig : IEntityTypeConfiguration<ExamAnswersStoredProcedure>
    //{
    //    public void Configure(EntityTypeBuilder<ExamAnswersStoredProcedure> builder)
    //    {
    //        builder.HasNoKey();

    //    }
    //}

    internal class ExamQuestionWithAnswerStoredProcedureConfig : IEntityTypeConfiguration<ExamQuestionWithAnswerStoredProcedure>
    {
        public void Configure(EntityTypeBuilder<ExamQuestionWithAnswerStoredProcedure> builder)
        {
            builder.HasNoKey();
            //builder.InsertUsingStoredProcedure("CreateExamAndQuestions", builder =>
            //{
            //    builder.HasResultColumn("ExamId");
            //    builder.HasResultColumn("QuestionId");
            //    builder.HasResultColumn("QuestionBody");
            //    builder.HasResultColumn("AnswerId");
            //    builder.HasResultColumn("AnswerBody");
            //});
        }
    }

    internal class ExamQuestionWithAnswerViewConfig : IEntityTypeConfiguration<ExamQuestionWithAnswerView>
    {
        public void Configure(EntityTypeBuilder<ExamQuestionWithAnswerView> builder)
        {
            builder.HasNoKey();
            builder.ToView("QuestionsWithAnswersByExamId");

        }
    }

    internal class ExamOverviewConfig : IEntityTypeConfiguration<ExamOverView>
    {
        public void Configure(EntityTypeBuilder<ExamOverView> builder)
        {
            builder.HasNoKey();

        }
    }
}
