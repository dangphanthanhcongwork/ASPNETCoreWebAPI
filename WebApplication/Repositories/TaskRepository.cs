namespace WebApplication.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly List<Models.Task> _tasks = new List<Models.Task>();

        public TaskRepository()
        {
            // add your dummy data here
            _tasks.Add(new Models.Task { Id = GenerateUniqueGuid(), Title = "Task 1", IsCompleted = true });
            _tasks.Add(new Models.Task { Id = GenerateUniqueGuid(), Title = "Task 2", IsCompleted = true });
            _tasks.Add(new Models.Task { Id = GenerateUniqueGuid(), Title = "Task 3", IsCompleted = false });
            _tasks.Add(new Models.Task { Id = GenerateUniqueGuid(), Title = "Task 4", IsCompleted = false });
            _tasks.Add(new Models.Task { Id = GenerateUniqueGuid(), Title = "Task 5", IsCompleted = false });
        }

        public async Task<Models.Task> GetTask(Guid id)
        {
            var task = _tasks.Find(t => t.Id == id) ?? throw new Exception("Task not found");
            return await System.Threading.Tasks.Task.FromResult(task);
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasks()
        {
            return await System.Threading.Tasks.Task.FromResult(_tasks.AsEnumerable());
        }

        public async Task<Models.Task> Add(Models.Task task)
        {
            _tasks.Add(task);
            return await System.Threading.Tasks.Task.FromResult(task);
        }

        public async Task<Models.Task> Update(Models.Task task)
        {
            var index = _tasks.FindIndex(existingTask => existingTask.Id == task.Id);
            _tasks[index] = task;
            return await System.Threading.Tasks.Task.FromResult(task);
        }

        public async System.Threading.Tasks.Task Delete(Guid id)
        {
            var task = await GetTask(id);
            _tasks.Remove(task);
        }

        public async Task<bool> CheckIfGuidExists(Guid id)
        {
            return await System.Threading.Tasks.Task.FromResult(_tasks.Any(t => t.Id == id));
        }

        public async Task<IEnumerable<Models.Task>> BulkAdd(IEnumerable<Models.Task> tasks)
        {
            foreach (var task in tasks)
            {
                task.Id = GenerateUniqueGuid();
                _tasks.Add(task);
            }

            return await System.Threading.Tasks.Task.FromResult(tasks);
        }

        public async System.Threading.Tasks.Task BulkDelete(IEnumerable<Guid> ids)
        {
            _tasks.RemoveAll(task => ids.Contains(task.Id));
            await System.Threading.Tasks.Task.CompletedTask;
        }

        private Guid GenerateUniqueGuid()
        {
            Guid newGuid;
            bool exists;

            do
            {
                newGuid = Guid.NewGuid();
                exists = _tasks.Any(t => t.Id == newGuid);
            } while (exists);

            return newGuid;
        }
    }
}
