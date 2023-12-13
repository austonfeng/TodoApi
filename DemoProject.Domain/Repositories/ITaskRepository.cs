using Task = System.Threading.Tasks.Task;

namespace DemoProject.Domain.Repositories
{
    public interface ITaskRepository : IDisposable
    {
        Task<IEnumerable<Entities.Task>> GetTasks();
        Task<Entities.Task> GetTasksByID(int taskId);
        Task InsertTask(Entities.Task task);
        Task DeleteTask(int taskId);
        void UpdateTask(Entities.Task task);
        Task Save();
    }
}
