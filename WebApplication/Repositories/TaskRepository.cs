using Microsoft.EntityFrameworkCore;
using WebApplication.Data;

namespace WebApplication.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskContext _context;

        public TaskRepository(TaskContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Task>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Models.Task> GetTask(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id) ?? throw new Exception("Not found!!!");
            return task;
        }

        public async Task PutTask(Guid id, Models.Task task)
        {
            _context.Tasks.Entry(task).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TaskExists(id))
                {
                    throw new Exception("Not found!!!");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task PostTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTask(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id) ?? throw new Exception("Not found!!!");
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> TaskExists(Guid id)
        {
            return await _context.Tasks.AnyAsync(e => e.Id == id);
        }

        public async Task BulkPostTasks(List<Models.Task> tasks)
        {
            _context.Tasks.AddRange(tasks);
            await _context.SaveChangesAsync();
        }

        public async Task BulkDeleteTasks(List<Guid> ids)
        {
            var tasks = _context.Tasks.Where(t => ids.Contains(t.Id));
            _context.Tasks.RemoveRange(tasks);
            await _context.SaveChangesAsync();
        }
    }
}