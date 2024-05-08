using WebApplication.Repositories;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.MapGet("/api/tasks/{id}", (ITaskRepository taskRepository, Guid id) => taskRepository.GetTask(id))
   .WithName("GetTask")
   .WithOpenApi();

app.MapGet("/api/tasks", (ITaskRepository taskRepository) => taskRepository.GetAllTasks())
   .WithName("GetAllTasks")
   .WithOpenApi();

app.MapPost("/api/tasks", (ITaskRepository taskRepository, WebApplication.Models.Task task) => taskRepository.Add(task))
   .WithName("AddTask")
   .WithOpenApi();

app.MapPost("/api/tasks/BulkAdd", (ITaskRepository taskRepository, IEnumerable<WebApplication.Models.Task> tasks) => taskRepository.BulkAdd(tasks))
.WithName("BulkAddTasks")
.WithOpenApi();

app.MapPut("/api/tasks/{id}", (ITaskRepository taskRepository, Guid id, WebApplication.Models.Task task) => taskRepository.Update(task))
   .WithName("UpdateTask")
   .WithOpenApi();

app.MapDelete("/api/tasks/{id}", (ITaskRepository taskRepository, Guid id) => taskRepository.Delete(id))
   .WithName("DeleteTask")
   .WithOpenApi();

app.MapDelete("/api/tasks/BulkDelete", (ITaskRepository taskRepository, IEnumerable<Guid> ids) => taskRepository.BulkDelete(ids))
   .WithName("BulkDeleteTasks")
   .WithOpenApi();

app.Run();
