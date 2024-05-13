using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks()
        {
            var tasks = await _taskService.GetAllTasks();

            return Ok(tasks);
        }

        // GET: api/Tasks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(Guid id)
        {
            try
            {
                var task = await _taskService.GetTask(id);
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

            if (!await _taskService.CheckIfGuidExists(id))
            {
                return NotFound();
            }

            var updatedTask = await _taskService.Update(task);
            return Ok(updatedTask);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(Models.Task task)
        {
            var createdTask = await _taskService.Add(task);

            return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
        }

        // DELETE: api/Tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            if (!await _taskService.CheckIfGuidExists(id))
            {
                return NotFound();
            }

            await _taskService.Delete(id);
            return NoContent();
        }

        // POST: api/Tasks/BulkAdd
        [HttpPost("BulkAdd")]
        public async Task<ActionResult<IEnumerable<Models.Task>>> BulkAddTasks(IEnumerable<Models.Task> tasks)
        {
            var createdTasks = await _taskService.BulkAdd(tasks);

            return Ok(createdTasks);
        }

        // DELETE: api/Tasks/BulkDelete
        [HttpDelete("BulkDelete")]
        public async Task<IActionResult> BulkDeleteTasks(IEnumerable<Guid> ids)
        {
            await _taskService.BulkDelete(ids);
            return NoContent();
        }
    }
}
