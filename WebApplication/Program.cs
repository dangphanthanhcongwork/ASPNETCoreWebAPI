using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Profiles;
using WebApplication.Repositories;
using WebApplication.Services;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<TaskContext>(opt =>
    opt.UseInMemoryDatabase("TaskList"));
builder.Services.AddAutoMapper(typeof(TaskProfile));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

SeedData(app);

app.Run();

void SeedData(Microsoft.AspNetCore.Builder.WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<TaskContext>();
        context.Database.EnsureCreated();
        if (!context.Tasks.Any())
        {
            context.Tasks.AddRange(
                new WebApplication.Models.Task { Name = "tỏ tình Linh", IsComplete = true },
                new WebApplication.Models.Task { Name = "tán tỉnh Phương", IsComplete = true },
                new WebApplication.Models.Task { Name = "làm quen Hà", IsComplete = false }
            );
            context.SaveChanges();
        }
    }
}