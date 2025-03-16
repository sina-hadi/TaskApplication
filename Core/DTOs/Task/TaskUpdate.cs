namespace Core.DTOs.Task
{
    public class TaskUpdate
    {
        public string? Title { get; set; } 
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
