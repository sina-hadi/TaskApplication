namespace Core.DTOs.Task
{
    public class TaskCreate
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
