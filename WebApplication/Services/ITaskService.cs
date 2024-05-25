using WebApplication.DTOs;

namespace WebApplication.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<Models.Task>> GetTasks();
        Task<Models.Task> GetTask(Guid id);
        Task PutTask(Guid id, TaskDTO task);
        Task PostTask(TaskDTO taskDTO);
        Task DeleteTask(Guid id);
        Task BulkPostTasks(List<TaskDTO> taskDTOs);
        Task BulkDeleteTasks(List<Guid> ids);
    }
}