namespace WebApplication.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Models.Task>> GetTasks();
        Task<Models.Task> GetTask(Guid id);
        Task PutTask(Guid id, Models.Task task);
        Task PostTask(Models.Task task);
        Task DeleteTask(Guid id);
        Task<bool> TaskExists(Guid id);
        Task BulkPostTasks(List<Models.Task> tasks);
        Task BulkDeleteTasks(List<Guid> ids);
    }
}