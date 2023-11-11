
using ExamSystem.API.Extensions;
using ExamSystem.Application;
using ExamSystem.Application.Services;
using ExamSystem.Infrastructure;
using ExamSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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


            builder.Services.AddInfrastructureDependencies().AddApplicationDependencies();

            builder.Services.AddScoped<IQuestionService, QuestionService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            await app.UpdateDatabase();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}