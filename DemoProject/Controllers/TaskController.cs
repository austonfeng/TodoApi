using DemoProject.Service.Interfaces;
using DemoProject.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService) => _taskService = taskService;


        /// <summary>
        /// Get All Tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(200, Type = typeof(object))]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetTasks();
            return Ok(tasks);
        }

        /// <summary>
        /// Get tasks based on days criteria
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLastNDaysTasks")]
        [ProducesResponseType(200, Type = typeof(object))]
        public async Task<IActionResult> GetLastNDaysTasks(int days = 7)
        {
            var tasks = await _taskService.GetLastNDaysTasks(days);
            return Ok(tasks);
        }

        /// <summary>
        /// Inserts a new task
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("AddTask")]
        [ProducesResponseType(200, Type = typeof(object))]
        public async Task<IActionResult> AddTask(TaskModel taskModel)
        {
            await _taskService.InsertTask(taskModel);
            return Ok();
        }

        /// <summary>
        /// Completes a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("MarkTaskAsCompleted/{taskId}")]
        public async Task<IActionResult> CompleteTask(int taskId)
        {
            var isTaskCompleted = await _taskService.CompleteTask(taskId);
            return Ok(isTaskCompleted);
        }
    }
}
