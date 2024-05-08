namespace WebApplication.Repositories
{
    public interface ITaskRepository
    {
        Task<Models.Task> GetTask(Guid id);
        Task<IEnumerable<Models.Task>> GetAllTasks();
        Task<Models.Task> Add(Models.Task task);
        Task<Models.Task> Update(Models.Task task);
        Task Delete(Guid id);
        Task<IEnumerable<Models.Task>> BulkAdd(IEnumerable<Models.Task> tasks);
        Task BulkDelete(IEnumerable<Guid> ids);
        Task<bool> CheckIfGuidExists(Guid id);
    }
}
