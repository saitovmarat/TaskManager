using TaskManager.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddApplicationPart(typeof(TaskController).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();


// Working 
var tasks = new[]
{
    new TaskManager.Models.Task("Task 1", false, "Description for Task 1", 1),
    new TaskManager.Models.Task("Task 2", false, "Description for Task 2", 2),
    new TaskManager.Models.Task("Task 3", false, "Description for Task 3", 3),
    new TaskManager.Models.Task("Task 4", false, "Description for Task 4", 4),
    new TaskManager.Models.Task("Task 5", false, "Description for Task 5", 5)
};

app.MapGet("/tasks", () =>
{
    return tasks;
})
.WithName("GetTasks")
.WithOpenApi();

app.Run();
