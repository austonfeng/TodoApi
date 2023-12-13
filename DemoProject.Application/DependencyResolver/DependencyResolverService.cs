

using DemoProject.Domain.Context;
using DemoProject.Domain.Repositories;
using DemoProject.Service.Interfaces;
using DemoProject.Service.Services;
using Microsoft.Extensions.DependencyInjection;


namespace DemoProject.Service.DependencyResolver
{
    public static class DependencyResolverService
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IDemoDbContext, DemoDbContext>();
        }
    }
}