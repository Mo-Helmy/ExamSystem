using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ExamSystem.Application.Services;
using ExamSystem.Application.Services.Contract;
using ExamSystem.Infrastructure.Repositories;
using ExamSystem.Infrastructure.Repositories.Contract;
using ExamSystem.Infrastructure.UnitOfWorks;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExamSystem.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ICertificateService, CertificateService>();
            services.AddScoped<ICertificateTopicService, CertificateTopicService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IExamQuestionService, ExamQuestionService>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IMailService, MailService>();


            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddFluentValidation(options =>
            {
                //Validate child properties and root collection elements
                options.ImplicitlyValidateChildProperties = true;
                options.ImplicitlyValidateRootCollectionElements = true;

                //Automatic registration of validators in assembly
                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            return services;
        }
    }
}
