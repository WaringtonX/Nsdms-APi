using mersetaWebAPI.CustomModels.Task;
using mersetaWebAPI.Interface;
using mersetaWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace mersetaWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class TaskController : Controller
    {
        private ITasks _tasks;
        public TaskController(ITasks tasks)
        {

            _tasks = tasks;
        }

        [HttpPost("CreateBacthModerationTask")]
        public async Task<ActionResult> CreateBacthModerationTask(TaskResponse taskResponse)
        {
            try
            {
                var response = await _tasks.CreateTaskForBacthModeration(taskResponse);
                if (response.Succeeded)
                {
                    return Ok(response.Data);
                }
                else
                {
                    return Ok(response.Message);
                }
                throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAGetAssessmentTasks")]
        public async Task<ActionResult> GetAssessmentTask()
        {
            var response = await _tasks.GetAssessmentTask();
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            return NotFound("User not found");
        }

        //update
        [HttpPut("UpdateTaskStatus")]
        public async Task<ActionResult> UpdateTaskStatus(long taskid, int status)
        {
            try
            {
                var user = await _tasks.UpdateTaskStatus(taskid, status);
                if (user.Succeeded)
                {
                    return Ok(user.Data);
                }
                throw new Exception(user.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("CreateTaskUser")]
        public async Task<ActionResult> CreateTaskUser(TaskUser taskResponse)
        {
            try
            {
                var response = await _tasks.CreateTaskUser(taskResponse);
                if (response.Succeeded)
                {
                    return Ok(response.Data);
                }
                else
                {
                    return Ok(response.Message);
                }
                throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTaskUser")]
        public async Task<ActionResult> GetTaskUser(long userid)
        {
            var response = await _tasks.GetTaskUser(userid);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            return NotFound("User not found");

        }

        [HttpGet("GetuserTasks")]
        public async Task<ActionResult> GetuserTasks(long userid)
        {
            var response = await _tasks.GetuserTasks(userid);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            return NotFound("User not found");
        }

        [HttpGet("GetSingleTasks")]
        public async Task<ActionResult> GetSingleTasks(long taskid)
        {
            var response = await _tasks.GetSingleTasks(taskid);
            if (response.Succeeded)
            {
                return Ok(response.Data);
            }
            return NotFound("User not found");
        }
    }
}
