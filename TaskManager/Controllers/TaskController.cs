using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly List<TaskManager.Models.Task> tasks = [];

        public TaskController()
        {
            tasks.Add(new TaskManager.Models.Task("Task 1", false, "Description for Task 1", 1));
            tasks.Add(new TaskManager.Models.Task("Task 2", false, "Description for Task 2", 2));
            tasks.Add(new TaskManager.Models.Task("Task 3", false, "Description for Task 3", 3));
        }

        [HttpGet("GetAllTasks")]
        public ActionResult<IEnumerable<TaskManager.Models.Task>> GetAllTasks()
        {
            return Ok(tasks);
        }

        [HttpGet("GetTaskById")]
        public ActionResult<TaskManager.Models.Task> GetTaskById(int id)
        {
            TaskManager.Models.Task? task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost("CreateTask")]
        public ActionResult<TaskManager.Models.Task> CreateTask(TaskManager.Models.Task task)
        {
            tasks.Add(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask(int id, TaskManager.Models.Task updatedTask)
        {
            TaskManager.Models.Task? task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            task.Name = updatedTask.Name;
            task.IsDone = updatedTask.IsDone;
            task.Description = updatedTask.Description;
            return NoContent();
        }

        [HttpDelete("DeleteTask")]
        public IActionResult DeleteTask(int id)
        {
            TaskManager.Models.Task? task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            tasks.Remove(task);
            return NoContent();
        }
    }
}
