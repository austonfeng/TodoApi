using DemoProject.Service.Models;

namespace DemoProject.Service.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel?>> GetTasks();
        Task<IEnumerable<TaskModel?>> GetLastNDaysTasks(int days);
        Task<TaskModel?> GetTasksByID(int taskId);
        Task InsertTask(TaskModel task);
        Task DeleteTask(int taskId);
        Task UpdateTask(TaskModel task);
        Task<bool> CompleteTask(int taskId);
        Task Save();
    }
}
