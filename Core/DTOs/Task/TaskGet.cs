namespace Core.DTOs.Task
{
    public class TaskGet
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DueDate { get; set; }
    }
}
