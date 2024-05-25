using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Task> Tasks { get; set; }
    }
}