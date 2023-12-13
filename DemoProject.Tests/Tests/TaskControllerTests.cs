using DemoProject.Api.Controllers;
using DemoProject.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DemoProject.Tests.Tests
{
    public class TaskTestsController
    {
        private readonly Mock<ITaskService> taskService;
        public TaskTestsController()
        {
            taskService = new Mock<ITaskService>();
        }

        [Fact]
        public async Task GetAllTasksTest_ReturnsOk()
        {
            //arrange

            var taskController = new TaskController(taskService.Object);

            //act
            var taskResult = await taskController.GetAll();
            var okResult = taskResult as OkObjectResult;

            //assert
            Assert.NotNull(okResult);

            Assert.Equal(200, okResult.StatusCode);
        }


        [Fact]
        public async Task GetLastNDaysTasksTest_ReturnsOk()
        {
            //arrange

            var taskController = new TaskController(taskService.Object);

            //act
            var taskResult = await taskController.GetLastNDaysTasks(7);
            var okResult = taskResult as OkObjectResult;

            //assert
            Assert.NotNull(okResult);

            Assert.Equal(200, okResult.StatusCode);
        }



        [Fact]
        public async Task MarkTaskAsCompletedTest_ReturnsOk()
        {
            //arrange

            var taskController = new TaskController(taskService.Object);

            //act
            var taskResult = await taskController.CompleteTask(2);
            var okResult = taskResult as OkObjectResult;

            //assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
