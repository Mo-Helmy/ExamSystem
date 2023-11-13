using ExamSystem.Domain.Entities.Identity;

namespace ExamSystem.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        // common attributes
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = null!;
        public string? CategoryDescription { get; set; }
    }

    public class Topic : BaseEntity
    {
        public string TopicName { get; set; } = null!;
        public string? TopicDescription { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Certificate> Certificates { get; set; }
    }

    public class Question : BaseEntity
    {
        public string QuestionBody { get; set; } = null!;

        public ICollection<Answer> Answers { get; set; } = new List<Answer>();

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }

    public class Answer : BaseEntity
    {
        public string AnswerBody { get; set; } = null!;
        public bool IsRight { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }

    public class Certificate : BaseEntity
    {
        public string CertificateName { get; set; } = null!;
        public int TestDurationInMinutes { get; set; }
        public decimal PassScore { get; set; }

        public ICollection<CertificateTopic> CertificateTopis { get; set;}

        public ICollection<Topic> Topics { get; set;}
    }

    public class CertificateTopic : BaseEntity
    {
        public int CertificateId { get; set; }
        public Certificate Certificate { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        public int QuestionCount { get; set; }
        public decimal TopicPercentage { get; set; }
    }

    public class Exam : BaseEntity
    {
        public int CertificateId { get; set; }
        public Certificate Certificate { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public DateTime ExamStartTime { get; set; }
        public DateTime ExamEndTime { get; set; }
        public DateTime? ExamCompletedTime { get; set; }
    }

    public class ExamQuestion : BaseEntity
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }


}


