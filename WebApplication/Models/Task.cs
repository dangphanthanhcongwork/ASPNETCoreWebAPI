namespace WebApplication.Models
{
    public class Task : ITask
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}