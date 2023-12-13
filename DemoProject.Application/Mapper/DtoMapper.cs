using DemoProject.Service.Models;

namespace DemoProject.Service.Mapper
{
    public class DtoMapper
    {
        public static TaskModel? MapTaskData(Domain.Entities.Task task)
        {
            if (task == null)
                return null;
            return new TaskModel
            {
                Active = task.Active,
                AssignedBy = task.AssignedBy,
                AssignedTo = task.AssignedTo,
                Comments = task.Comments,
                CompletedBy = task.CompletedBy,
                DateCompleted = task.DateCompleted,
                DateCreated = task.DateCreated,
                DateDue = task.DateDue,
                Id = task.Id,
                Priority = task.Priority,
                Status = task.Status,
                TaskDetails = task.TaskDetails,
                TaskTitle = task.TaskTitle
            };
        }

        public static Domain.Entities.Task? MapTaskModelData(TaskModel task)
        {
            if (task == null)
                return null;
            return new Domain.Entities.Task
            {
                Active = true,
                AssignedBy = task.AssignedBy,
                AssignedTo = task.AssignedTo,
                Comments = task.Comments,
                CompletedBy = task.CompletedBy,
                DateCompleted = task.DateCompleted,
                DateCreated = DateTime.Now,
                DateDue = task.DateDue,
                Priority = task.Priority,
                Status = task.Status,
                TaskDetails = task.TaskDetails,
                TaskTitle = task.TaskTitle
            };
        }
    }
}
