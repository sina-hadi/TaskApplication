namespace Core.DTOs.Task
{
    public class TaskUpdateRepo
    {
        public required string Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
