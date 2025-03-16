namespace Core.DTOs.Task
{
    public class TaskCreateRepo
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
    }
}
