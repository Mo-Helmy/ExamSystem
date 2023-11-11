using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExamSystem.Application.Services;
using ExamSystem.Infrastructure.Repositories;
using ExamSystem.Infrastructure.Repositories.Contract;
using ExamSystem.Infrastructure.UnitOfWorks;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace ExamSystem.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IQuestionService, QuestionService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
