using AutoMapper;
using WebApplication.DTOs;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Models.Task>> GetTasks()
        {
            return await _repository.GetTasks();
        }

        public async Task<Models.Task> GetTask(Guid id)
        {
            try
            {
                return await _repository.GetTask(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PutTask(Guid id, TaskDTO taskDTO)
        {
            try
            {
                var task = _mapper.Map<Models.Task>(taskDTO);
                task.Id = id;
                await _repository.PutTask(id, task);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PostTask(TaskDTO taskDTO)
        {
            var task = _mapper.Map<Models.Task>(taskDTO);
            await _repository.PostTask(task);
        }

        public async Task DeleteTask(Guid id)
        {
            try
            {
                await _repository.DeleteTask(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task BulkPostTasks(List<TaskDTO> taskDTOs)
        {
            var tasks = _mapper.Map<List<Models.Task>>(taskDTOs);
            await _repository.BulkPostTasks(tasks);
        }

        public async Task BulkDeleteTasks(List<Guid> ids)
        {
            await _repository.BulkDeleteTasks(ids);
        }
    }
}