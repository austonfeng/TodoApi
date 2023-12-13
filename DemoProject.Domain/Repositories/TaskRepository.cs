using DemoProject.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Domain.Repositories
{
    public class TaskRepository : ITaskRepository, IDisposable
    {
        private DemoDbContext _context;
        public TaskRepository(DemoDbContext context)
        {
            _context = context;
        }
        public async Task DeleteTask(int taskId)
        {
            Entities.Task task = await _context.Tasks.FindAsync(taskId);
            _context.Tasks.Remove(task);
        }

        public async Task<Entities.Task> GetTasksByID(int taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }

        public async Task<IEnumerable<Entities.Task>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task InsertTask(Entities.Task task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateTask(Entities.Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
