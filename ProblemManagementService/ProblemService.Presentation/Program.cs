
using Microsoft.EntityFrameworkCore;
using ProblemService.Application.Service.Implementations;
using ProblemService.Application.Service.Interfaces;
using ProblemService.Infrastructure.Context;
using ProblemService.Infrastructure.UnitOfWork;

namespace ProblemService.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<PMContext>(ops => ops.UseSqlServer(builder.Configuration.GetConnectionString("PMConnection")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IProblemService, ProblemService.Application.Service.Implementations.ProblemService>();
            builder.Services.AddTransient<ITagService, TagService>();
            //auto mapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
