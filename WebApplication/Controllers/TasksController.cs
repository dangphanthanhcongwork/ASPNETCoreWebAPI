using Microsoft.AspNetCore.Mvc;
using WebApplication.DTOs;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _service.GetTasks();
            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            try
            {
                var task = await _service.GetTask(id);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/tasks/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(Guid id, TaskDTO taskDTO)
        {
            try
            {
                await _service.PutTask(id, taskDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostTask(TaskDTO taskDTO)
        {
            await _service.PostTask(taskDTO);
            return CreatedAtAction(nameof(GetTask), new { Id = Guid.NewGuid() }, taskDTO);
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            try
            {
                await _service.DeleteTask(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/tasks/bulk-post
        [HttpPost("bulk-post")]
        public async Task<IActionResult> BulkPostTasks(List<TaskDTO> taskDTOs)
        {
            await _service.BulkPostTasks(taskDTOs);
            return CreatedAtAction(nameof(GetTasks), taskDTOs);
        }

        // DELETE: api/tasks/bulk-delete
        [HttpDelete("bulk-delete")]
        public async Task<IActionResult> BulkDeleteTasks(List<Guid> ids)
        {
            await _service.BulkDeleteTasks(ids);
            return NoContent();
        }

    }
}