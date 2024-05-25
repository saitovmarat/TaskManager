using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaskManager.Models;

namespace TaskManager.Interfaces{
    public interface IController{
        [HttpGet("GetAllTasks")]
        public ActionResult<IEnumerable<BarsGroupTask>> GetAllTasks();

        [HttpGet("GetTaskById")]
        public ActionResult<BarsGroupTask> GetTaskById(int id);

        [HttpPost("CreateTask")]
        public ActionResult<BarsGroupTask> CreateTask(BarsGroupTask task);

        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask(int id, BarsGroupTask updatedTask);

        [HttpDelete("DeleteTask")]
        public IActionResult DeleteTask(int id);
    }
}

