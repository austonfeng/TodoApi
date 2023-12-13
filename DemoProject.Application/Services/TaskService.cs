using DemoProject.Domain.Repositories;
using DemoProject.Service.Interfaces;
using DemoProject.Service.Mapper;
using DemoProject.Service.Models;
using System.Linq.Expressions;

namespace DemoProject.Service.Services
{
    public class TaskService : ITaskService
    {
        private readonly UnitOfWork unitOfWork = new();

        /// <summary>
        /// Deletes a task based on task id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task DeleteTask(int taskId)
        {
            Domain.Entities.Task task = await unitOfWork.TaskRepository.GetByID(taskId);
            if (task is not null)
            {
                unitOfWork.TaskRepository.Delete(task);
                unitOfWork.Save();
            }
        }


        /// <summary>
        /// Fetches all tasks
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TaskModel?>> GetTasks()
        {
            var tasks = await unitOfWork.TaskRepository.Get();
            return tasks.Select(Mapper.DtoMapper.MapTaskData).ToList();
        }


        /// <summary>
        /// Fetched tasks for last N days
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TaskModel?>> GetLastNDaysTasks(int days)
        {
            var nDaysAgo = DateTime.Today.AddDays(-days);
            Expression<Func<Domain.Entities.Task, bool>> where = a => a.DateCompleted >= nDaysAgo && a.DateCompleted <= DateTime.Now && a.DateCompleted != null;
            var tasks = await unitOfWork.TaskRepository.Get(where);
            return tasks.Select(Mapper.DtoMapper.MapTaskData).ToList();
        }


        /// <summary>
        /// Fetches a single task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<TaskModel?> GetTasksByID(int taskId)
        {
            var task = await unitOfWork.TaskRepository.GetByID(taskId);
            if (task is not null)
            {
                return Mapper.DtoMapper.MapTaskData(task);
            }
            return null;
        }


        /// <summary>
        /// Adds a new task into db
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task InsertTask(TaskModel task)
        {
            await unitOfWork.TaskRepository.Insert(Mapper.DtoMapper.MapTaskModelData(task));
            await Save();
        }


        /// <summary>
        /// saves a transaction
        /// </summary>
        /// <returns></returns>
        public async Task Save()
        {
            await unitOfWork.Save();
        }


        /// <summary>
        /// updates a task
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task UpdateTask(TaskModel task)
        {
            unitOfWork.TaskRepository.Update(DtoMapper.MapTaskModelData(task));
            await Save();
        }


        /// <summary>
        /// Completes a task based on task id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<bool> CompleteTask(int taskId)
        {
            var task = await unitOfWork.TaskRepository.GetByID(taskId);
            if (task != null)
            {
                task.DateCompleted = DateTime.Now;
                unitOfWork.TaskRepository.Update(task);
                await Save();
                return true;

            }
            return false;
        }
    }
}
