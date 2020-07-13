using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Helpers.Requests.Tasks;
using TaskManager.Helpers.Responses.Tasks;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<AddTaskResponse>> Add(AddTaskRequest request)
        {
            try
            {
                // var claims = Request.HttpContext.User.Claims;
                //var userId = claims.Where(c => c.Type == "Id").Single().Value;
                var userId = "1";
                var response = await _taskService.Add(new Data.Models.Task
                {
                    Title = request.Title,
                    Description = request.Description,
                    Deadline = request.Deadline,
                    IsActive = true
                }, request.Requests, int.Parse(userId));
                if (response.Task == null)
                {
                    return StatusCode(400);
                }
                else return response;
            } catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}