using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<CertificateTopic> CertificateTopics { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestion> ExamsQuestions { get; set; }

        public DbSet<AppUser> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ExamQuestion>(builder =>
            //{
            //    builder.HasOne(x => x.Question)
            //        .WithMany()
            //        .HasForeignKey(x => x.QuestionId)
            //        .OnDelete(DeleteBehavior.NoAction);

            //    builder.HasOne(x => x.Answer)
            //        .WithMany()
            //        .HasForeignKey(x => x.AnswerId)
            //        .OnDelete(DeleteBehavior.NoAction);

            //    builder.HasOne(x => x.Exam)
            //        .WithMany()
            //        .HasForeignKey(x => x.ExamId)
            //        .OnDelete(DeleteBehavior.NoAction);
            //});

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
