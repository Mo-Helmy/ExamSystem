using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamSystem.Infrastructure.Repositories;
using ExamSystem.Infrastructure.Repositories.Contract;
using ExamSystem.Infrastructure.UnitOfWorks;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using Microsoft.Extensions.DependencyInjection;

namespace ExamSystem.Infrastructure
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            // Not required as unit of work will generate new object of repository type when needed without using dependency injection.
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
