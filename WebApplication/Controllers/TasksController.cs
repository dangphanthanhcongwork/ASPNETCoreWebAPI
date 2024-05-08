using Microsoft.AspNetCore.Mvc;
using WebApplication.Repositories;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            var tasks = await _taskRepository.GetAllTasks();

            return Ok(tasks);
        }

        // GET: api/Tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(Guid id)
        {
            try
            {
                var task = await _taskRepository.GetTask(id);
                return Ok(task);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/Tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(Guid id, Models.Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            if (!await _taskRepository.CheckIfGuidExists(id))
            {
                return NotFound();
            }

            var updatedTask = await _taskRepository.Update(task);
            return Ok(updatedTask);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(Models.Task task)
        {
            task.Id = await GenerateUniqueGuid();
            var createdTask = await _taskRepository.Add(task);

            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        // DELETE: api/Tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            if (!await _taskRepository.CheckIfGuidExists(id))
            {
                return NotFound();
            }

            await _taskRepository.Delete(id);
            return NoContent();
        }

        // POST: api/Tasks/BulkAdd
        [HttpPost("BulkAdd")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> BulkAddTasks(IEnumerable<Models.Task> tasks)
        {
            var createdTasks = await _taskRepository.BulkAdd(tasks);

            return Ok(createdTasks);
        }

        // DELETE: api/Tasks/BulkDelete
        [HttpDelete("BulkDelete")]
        public async Task<IActionResult> BulkDeleteTasks(IEnumerable<Guid> ids)
        {
            await _taskRepository.BulkDelete(ids);
            return NoContent();
        }

        private async Task<Guid> GenerateUniqueGuid()
        {
            Guid newGuid;
            bool exists;

            do
            {
                newGuid = Guid.NewGuid();
                exists = await _taskRepository.CheckIfGuidExists(newGuid);
            } while (exists);

            return newGuid;
        }
    }
}
