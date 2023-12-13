namespace DemoProject.Service.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string? TaskDetails { get; set; }
        public string? TaskTitle { get; set; }
        public string? Priority { get; set; }
        public string? AssignedBy { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime? DateDue { get; set; }
        public bool? Active { get; set; }
        public string? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? CompletedBy { get; set; }
        public string? Comments { get; set; } = string.Empty;
    }
}
