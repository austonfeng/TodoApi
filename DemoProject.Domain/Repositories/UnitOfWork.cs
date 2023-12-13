using DemoProject.Domain.Context;

namespace DemoProject.Domain.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private DemoDbContext context = new DemoDbContext();
        private BaseRepository<Entities.Task>? taskRepository;


        public BaseRepository<Entities.Task> TaskRepository
        {
            get
            {

                if (this.taskRepository == null)
                {
                    this.taskRepository = new BaseRepository<Entities.Task>(context);
                }
                return taskRepository;
            }
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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
