
using ExamSystem.API.Extensions;
using ExamSystem.Application;
using ExamSystem.Application.Errors;
using ExamSystem.Application.Middlewares;
using ExamSystem.Application.Services;
using ExamSystem.Infrastructure;
using ExamSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using System.Reflection;
using ExamSystem.Application.Dtos.MailDtos;

namespace ExamSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services
                .AddApplicationDependencies()
                .AddInfrastructureDependencies();

            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(MailSettings.SectionKey));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value?.Errors.Count > 0)
                                            .SelectMany(x => x.Value.Errors)
                                            .Select(x => x.ErrorMessage)
                                            .ToList();

                    var validationErrorResponse = new ApiValidationErrorResponse() { Errors = errors };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });


            builder.Services.AddSwaggerServices();

            var app = builder.Build();

            await app.UpdateDatabase();

            app.UseSwaggerMiddlewares();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}