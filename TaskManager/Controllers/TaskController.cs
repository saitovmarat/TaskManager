using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Interfaces;
using TaskManager.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase, IController
    {
        private readonly ILogger<TaskController> _logger;
        private readonly List<BarsGroupTask> tasks = [];

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
            // Не знаю как настроить авторизацию
            string role = "Admin";
            if (role == "Admin")
            {
                tasks.Add(new BarsGroupTask("Task 1", false, "Admin Task 1", 1));
                tasks.Add(new BarsGroupTask("Task 2", false, "Admin Task 2", 2));
                tasks.Add(new BarsGroupTask("Task 3", false, "Admin Task 3", 3));
            }
            else
            {
                tasks.Add(new BarsGroupTask("Task 1", false, "User Task 1", 1));
                tasks.Add(new BarsGroupTask("Task 2", false, "User Task 2", 2));
                tasks.Add(new BarsGroupTask("Task 3", false, "User Task 3", 3));
            }
        }

        [Route("GetAllTasks")]
        [HttpGet]
        public ActionResult<IEnumerable<BarsGroupTask>> GetAllTasks()
        {
            //_logger.LogInformation("GetAllTasks called");
            return Ok(tasks);
        }

        [Route("GetTaskById")]
        [HttpGet]
        public ActionResult<BarsGroupTask> GetTaskById(int id)
        {
            //_logger.LogInformation("GetTaskById called");
            BarsGroupTask? task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [Route("CreateTask")]
        [HttpPost]
        public ActionResult<BarsGroupTask> CreateTask(BarsGroupTask task)
        {
            //_logger.LogInformation("CreateTask called");
            tasks.Add(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [Route("UpdateTask")]
        [HttpPut]
        public IActionResult UpdateTask(int id, BarsGroupTask updatedTask)
        {
            //_logger.LogInformation("UpdateTask called");
            BarsGroupTask? task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            task.Name = updatedTask.Name;
            task.IsDone = updatedTask.IsDone;
            task.Description = updatedTask.Description;
            return NoContent();
        }

        [Route("DeleteTask")]
        [HttpDelete]
        public IActionResult DeleteTask(int id)
        {
            //_logger.LogInformation("DeleteTask called");
            BarsGroupTask? task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            tasks.Remove(task);
            return NoContent();
        }

        [Route("SendEmail")]
        [HttpPost]
        public async Task<ActionResult> SendEmail(string recipient, string subject, string body)
        {
            //_logger.LogInformation("SendEmail called");
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Marat", "maratsajtov@gmail.com"));
            message.To.Add(new MailboxAddress("", recipient));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("email", "password");

                // Send the email
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            return Ok("Email sent successfully");
        }

        [Route("UploadFile")]
        [HttpPost]
        public async Task<ActionResult> UploadFile()
        {
            //_logger.LogInformation("UploadFile called");
            if (Request.Form.Files.Count == 0)
            {
                return BadRequest("No file uploaded");
            }
            var file = Request.Form.Files[0];
            var filePath = "tasks.txt";
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return Ok("File uploaded successfully");
        }
    }
}
