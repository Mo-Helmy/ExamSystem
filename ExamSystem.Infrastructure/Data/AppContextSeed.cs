using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Data
{
    public static class AppContextSeed
    {
        public static async Task AddSeedsAsync(AppDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            #region Rols
            if (!roleManager.Roles.Any())
            {
                //await _roleManager.CreateAsync(new IdentityRole("USER"));
                await roleManager.CreateAsync(new IdentityRole("USER"));
                await roleManager.CreateAsync(new IdentityRole("ADMIN"));
                await roleManager.CreateAsync(new IdentityRole("SUPERADMIN"));

                //await dbContext.SaveChangesAsync();
            } 
            #endregion

            #region Users
            if (!dbContext.Users.Any())
            {
                var users = new List<AppUser>()
                {
                    
                    new AppUser() {Id = "user1", FirstName = "foo", LastName = "roo",Email="user1@example.com", UserName = "username1", CreatedAt = DateTime.Now, IsDeleted = false},
                    new AppUser() {Id = "user2", FirstName = "foo", LastName = "too",Email="user2@example.com", UserName = "username2", CreatedAt = DateTime.Now, IsDeleted = false},
                    new AppUser() {Id = "admin1",FirstName = "foo", LastName = "yoo", Email="admin1@example.com", UserName = "admin1",CreatedAt = DateTime.Now, IsDeleted = false},
                    new AppUser() {Id = "admin2",FirstName = "foo", LastName = "hoo", Email="admin2@example.com", UserName = "admin2", CreatedAt = DateTime.Now, IsDeleted = false},
                    new AppUser() {Id = "admin3",FirstName = "foo", LastName = "goo", Email="admin3@example.com", UserName = "admin3", CreatedAt = DateTime.Now, IsDeleted = false},
                    new AppUser() {Id = "superadmin1",FirstName = "foo", LastName = "loo", Email="superadmin1@example.com", UserName = "superadmin1", CreatedAt = DateTime.Now, IsDeleted = false},
                };


                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "string123");

                    if (user.Id.Contains("user"))
                    {
                        await userManager.AddToRoleAsync(user, "USER");
                    }
                    else if (user.Id.Contains("superadmin"))
                    {
                        await userManager.AddToRoleAsync(user, "SUPERADMIN");
                    }
                    else if (user.Id.Contains("admin"))
                    {
                        await userManager.AddToRoleAsync(user, "ADMIN");
                    }
                }

            } 
            #endregion

            #region Categories
            if (!dbContext.Categories.Any())
            {
                var categories = new List<Category>()
                {
                    new Category {CategoryName = "Language", CategoryDescription = "All Languages Description", CreatedAt = DateTime.Now},
                    new Category {CategoryName = "Programing", CategoryDescription = "All Programing Description", CreatedAt = DateTime.Now},
                    new Category {CategoryName = "Engineering", CategoryDescription = "All Engineering Description", CreatedAt = DateTime.Now},
                    new Category {CategoryName = "Business", CategoryDescription = "All Business Description", CreatedAt = DateTime.Now},
                };
                await dbContext.Categories.AddRangeAsync(categories);
                await dbContext.SaveChangesAsync();
            }
            #endregion

            #region Topics
            if (!dbContext.Topics.Any())
            {
                var topics = new List<Topic>()
                {
                    new Topic {TopicName = "English", TopicDescription = "All English Description" , CategoryId = 1, CreatedAt= DateTime.Now},
                    new Topic {TopicName = "French", TopicDescription = "All English Description" , CategoryId = 1, CreatedAt= DateTime.Now},
                    new Topic {TopicName = "C#", TopicDescription = "All C# Description" , CategoryId = 2, CreatedAt= DateTime.Now},
                    new Topic {TopicName = "C++", TopicDescription = "All C++ Description" , CategoryId = 2, CreatedAt= DateTime.Now},
                    new Topic {TopicName = "Java", TopicDescription = "All Java Description" , CategoryId = 2, CreatedAt= DateTime.Now},
                    new Topic {TopicName = "Python", TopicDescription = "All Python Description" , CategoryId = 2, CreatedAt= DateTime.Now},
                    new Topic {TopicName = "React", TopicDescription = "All React Description" , CategoryId = 2, CreatedAt= DateTime.Now},
                    new Topic {TopicName = "Next.js", TopicDescription = "All Next Description" , CategoryId = 2, CreatedAt= DateTime.Now},
                    new Topic {TopicName = "JavaScript", TopicDescription = "All JavaScript Description" , CategoryId = 2, CreatedAt= DateTime.Now},
                };
                await dbContext.Topics.AddRangeAsync(topics);
                await dbContext.SaveChangesAsync();
            }
            #endregion

            #region Questions
            if (!dbContext.Questions.Any())
            {
                var questionsC = new List<Question>()
                {
                    new Question {QuestionBody = "C# Question no 1", CreatedAt = DateTime.Now, TopicId = 3, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "C# Question no 2", CreatedAt = DateTime.Now, TopicId = 3, Answers = new List<Answer>()
                        {
                            new Answer () { AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "C# Question no 3", CreatedAt = DateTime.Now, TopicId = 3, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 3 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C# Question no 4", CreatedAt = DateTime.Now, TopicId = 3, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 4 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C# Question no 5", CreatedAt = DateTime.Now, TopicId = 3, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 5 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C# Question no 6", CreatedAt = DateTime.Now, TopicId = 3, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 6 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C# Question no 7", CreatedAt = DateTime.Now, TopicId = 3, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 7 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C# Question no 8", CreatedAt = DateTime.Now, TopicId = 3, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 8 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C# Question no 9", CreatedAt = DateTime.Now, TopicId = 3, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 9 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C# Question no 10", CreatedAt = DateTime.Now, TopicId = 3, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 10 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                        },
                    },
                };

                var questionsCPlus = new List<Question>()
                {
                    new Question {QuestionBody = "C++ Question no 1", CreatedAt = DateTime.Now, TopicId = 4, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "C++ Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "C++ Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "C++ Question no 1 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "C++ Question no 2", CreatedAt = DateTime.Now, TopicId = 4, Answers = new List<Answer>()
                        {
                            new Answer () { AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "C++ Question no 3", CreatedAt = DateTime.Now, TopicId = 4, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 3 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C++ Question no 4", CreatedAt = DateTime.Now, TopicId = 4, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 4 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C++ Question no 5", CreatedAt = DateTime.Now, TopicId = 4, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 5 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C++ Question no 6", CreatedAt = DateTime.Now, TopicId = 4, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 6 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C++ Question no 7", CreatedAt = DateTime.Now, TopicId = 4, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 7 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C++ Question no 8", CreatedAt = DateTime.Now, TopicId = 4, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 8 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C++ Question no 9", CreatedAt = DateTime.Now, TopicId = 4, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 9 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "C# C++ Question no 10", CreatedAt = DateTime.Now, TopicId = 4, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "C++ Question no 10 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "C++ Question no 10 right answer", IsRight = true },
                            new Answer () {AnswerBody = "C++ Question no 10 wrong answer", IsRight = false },
                        },
                    },
                };

                var questionsJava = new List<Question>()
                {
                    new Question {QuestionBody = "Java Question no 1", CreatedAt = DateTime.Now, TopicId = 5, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "Java Question no 2", CreatedAt = DateTime.Now, TopicId = 5, Answers = new List<Answer>()
                        {
                            new Answer () { AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "Java Question no 3", CreatedAt = DateTime.Now, TopicId = 5, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 3 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Java Question no 4", CreatedAt = DateTime.Now, TopicId = 5, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 4 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Java Question no 5", CreatedAt = DateTime.Now, TopicId = 5, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 5 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Java Question no 6", CreatedAt = DateTime.Now, TopicId = 5, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 6 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Java Question no 7", CreatedAt = DateTime.Now, TopicId = 5, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 7 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Java Question no 8", CreatedAt = DateTime.Now, TopicId = 5, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 8 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Java Question no 9", CreatedAt = DateTime.Now, TopicId = 5, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 9 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Java Question no 10", CreatedAt = DateTime.Now, TopicId = 5, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 10 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                        },
                    },
                };

                var questionsPython = new List<Question>()
                {
                    new Question {QuestionBody = "Python Question no 1", CreatedAt = DateTime.Now, TopicId = 6, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "Python Question no 2", CreatedAt = DateTime.Now, TopicId = 6, Answers = new List<Answer>()
                        {
                            new Answer () { AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "Python Question no 3", CreatedAt = DateTime.Now, TopicId = 6, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 3 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Python Question no 4", CreatedAt = DateTime.Now, TopicId = 6, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 4 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Python Question no 5", CreatedAt = DateTime.Now, TopicId = 6, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 5 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Python Question no 6", CreatedAt = DateTime.Now, TopicId = 6, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 6 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Python Question no 7", CreatedAt = DateTime.Now, TopicId = 6, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 7 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Python Question no 8", CreatedAt = DateTime.Now, TopicId = 6, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 8 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Python Question no 9", CreatedAt = DateTime.Now, TopicId = 6, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 9 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Python Question no 10", CreatedAt = DateTime.Now, TopicId = 6, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 10 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                        },
                    },
                };

                var questionsReact = new List<Question>()
                {
                    new Question {QuestionBody = "React Question no 1", CreatedAt = DateTime.Now, TopicId = 7, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "React Question no 2", CreatedAt = DateTime.Now, TopicId = 7, Answers = new List<Answer>()
                        {
                            new Answer () { AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "React Question no 3", CreatedAt = DateTime.Now, TopicId = 7, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 3 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "React Question no 4", CreatedAt = DateTime.Now, TopicId = 7, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 4 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "React Question no 5", CreatedAt = DateTime.Now, TopicId = 7, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 5 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "React Question no 6", CreatedAt = DateTime.Now, TopicId = 7, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 6 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "React Question no 7", CreatedAt = DateTime.Now, TopicId = 7, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 7 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "React Question no 8", CreatedAt = DateTime.Now, TopicId = 7, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 8 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "React Question no 9", CreatedAt = DateTime.Now, TopicId = 7, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 9 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "React Question no 10", CreatedAt = DateTime.Now, TopicId = 7, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 10 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                        },
                    },
                };

                var questionsNext = new List<Question>()
                {
                    new Question {QuestionBody = "Next Question no 1", CreatedAt = DateTime.Now, TopicId = 8, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "Next Question no 2", CreatedAt = DateTime.Now, TopicId = 8, Answers = new List<Answer>()
                        {
                            new Answer () { AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "Next Question no 3", CreatedAt = DateTime.Now, TopicId = 8, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 3 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Next Question no 4", CreatedAt = DateTime.Now, TopicId = 8, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 4 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Next Question no 5", CreatedAt = DateTime.Now, TopicId = 8, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 5 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Next Question no 6", CreatedAt = DateTime.Now, TopicId = 8, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 6 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Next Question no 7", CreatedAt = DateTime.Now, TopicId = 8, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 7 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Next Question no 8", CreatedAt = DateTime.Now, TopicId = 8, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 8 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Next Question no 9", CreatedAt = DateTime.Now, TopicId = 8, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 9 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Next Question no 10", CreatedAt = DateTime.Now, TopicId = 8, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 10 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                        },
                    },
                };

                var questionsJavaScript = new List<Question>()
                {
                    new Question {QuestionBody = "Javascript Question no 1", CreatedAt = DateTime.Now, TopicId = 9, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 1 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "Javascript Question no 2", CreatedAt = DateTime.Now, TopicId = 9, Answers = new List<Answer>()
                        {
                            new Answer () { AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 2 right answer", IsRight = true },
                        },
                    },
                    new Question { QuestionBody = "Javascript Question no 3", CreatedAt = DateTime.Now, TopicId = 9, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 3 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 3 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Javascript Question no 4", CreatedAt = DateTime.Now, TopicId = 9, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 4 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 4 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Javascript Question no 5", CreatedAt = DateTime.Now, TopicId = 9, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 5 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 5 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Javascript Question no 6", CreatedAt = DateTime.Now, TopicId = 9, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 6 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 6 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Javascript Question no 7", CreatedAt = DateTime.Now, TopicId = 9, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 7 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 7 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Javascript Question no 8", CreatedAt = DateTime.Now, TopicId = 9, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 8 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 8 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Javascript Question no 9", CreatedAt = DateTime.Now, TopicId = 9, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 9 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 9 wrong answer", IsRight = false },
                        },
                    },
                    new Question { QuestionBody = "Javascript Question no 10", CreatedAt = DateTime.Now, TopicId = 9, Answers = new List<Answer>()
                        {
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                            new Answer () {AnswerBody = "Question no 10 right answer", IsRight = true },
                            new Answer () {AnswerBody = "Question no 10 wrong answer", IsRight = false },
                        },
                    },
                };

                await dbContext.Questions.AddRangeAsync(questionsC);
                await dbContext.Questions.AddRangeAsync(questionsC);
                await dbContext.Questions.AddRangeAsync(questionsCPlus);
                await dbContext.Questions.AddRangeAsync(questionsCPlus);
                await dbContext.Questions.AddRangeAsync(questionsJava);
                await dbContext.Questions.AddRangeAsync(questionsJava);
                await dbContext.Questions.AddRangeAsync(questionsPython);
                await dbContext.Questions.AddRangeAsync(questionsPython);
                await dbContext.Questions.AddRangeAsync(questionsReact);
                await dbContext.Questions.AddRangeAsync(questionsReact);
                await dbContext.Questions.AddRangeAsync(questionsNext);
                await dbContext.Questions.AddRangeAsync(questionsNext);
                await dbContext.Questions.AddRangeAsync(questionsJavaScript);
                await dbContext.Questions.AddRangeAsync(questionsJavaScript);

                await dbContext.SaveChangesAsync();
            }
            #endregion

            #region Certificates
            if (!dbContext.Certificates.Any())
            {
                var certificates = new List<Certificate>()
                {
                    new Certificate {CertificateName = "Programing Language Certificate", TestDurationInMinutes = 45, PassScore = 0.7m, CertificateTopis = new List<CertificateTopic> ()
                    {
                        new CertificateTopic { CertificateId = 1, TopicId = 3, QuestionCount = 3, TopicPercentage = 0.4m },
                        new CertificateTopic { CertificateId = 1, TopicId = 4, QuestionCount = 3, TopicPercentage = 0.2m },
                        new CertificateTopic { CertificateId = 1, TopicId = 5, QuestionCount = 3, TopicPercentage = 0.2m },
                        new CertificateTopic { CertificateId = 1, TopicId = 6, QuestionCount = 3, TopicPercentage = 0.2m },
                    } },
                    new Certificate {CertificateName = "ٌٌReact.js Developer", TestDurationInMinutes = 45, PassScore = 0.7m, CertificateTopis = new List<CertificateTopic> ()
                    {
                        new CertificateTopic { CertificateId = 2, TopicId = 7, QuestionCount = 6, TopicPercentage = 0.5m },
                        new CertificateTopic { CertificateId = 2, TopicId = 8, QuestionCount = 2, TopicPercentage = 0.25m },
                        new CertificateTopic { CertificateId = 2, TopicId = 9, QuestionCount = 2, TopicPercentage = 0.25m },
                    } },
                };
                await dbContext.Certificates.AddRangeAsync(certificates);
                await dbContext.SaveChangesAsync();
            }
            #endregion
        }
    }
}

