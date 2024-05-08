namespace WebApplication.Models
{
    public interface ITask
    {
        Guid Id { get; set; }
        string? Title { get; set; }
        bool IsCompleted { get; set; }
    }
}
