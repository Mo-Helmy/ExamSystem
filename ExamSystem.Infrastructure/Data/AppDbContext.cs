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


        //database stored procedure
        public DbSet<ExamQuestionWithAnswerStoredProcedure> ExamQuestionWithAnswerStoredProcedures { get; set; }
        public DbSet<ExamOverView> ExamOverViews { get; set; }
        
        // database View
        public DbSet<ExamQuestionWithAnswerView> ExamQuestionWithAnswerViews { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //builder.Ignore<ExamQuestionWithAnswerStoredProcedure>();
            //builder.Ignore<ExamOverView>();

            base.OnModelCreating(builder);
        }



        [DbFunction("GetTotalSuccessPercentage", Schema = "dbo")]
        public static int GetTotalSuccessPercentage(int examId)
        {
            // This doesn't need an implementation; EF core uses the function mapping
            throw new NotImplementedException();
        }
    }
}
