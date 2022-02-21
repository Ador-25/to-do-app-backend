using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Body;
using toDoListApi.Data;

namespace toDoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        ITaskData _taskData;

        public TaskController(ITaskData taskData)
        {
            _taskData = taskData;
        }
        // GET ALL TASK
        [Authorize]
        [HttpGet]
        public IActionResult GetSubTasks()
        {
            return Ok(_taskData.GetTaskList(User.Identity.Name));
        }

        // ADD TASK
        [Authorize]
        [HttpPost]
        public IActionResult AddTask([FromBody] TaskModel taskModel)
        {
            var data = _taskData.AddTask(User.Identity.Name, taskModel);
            if (data != null)
                return Ok(data);
            else
                return Ok(new Response {Status="Insert Error",Message="Could not add data for clashing time" });

        }
        // GET TASK IN A RANGE
        [Authorize]
        [HttpGet]
        [Route("range")]
        public IActionResult GetSubTasksInRange([FromBody] RangeModel model)
        {
            return Ok(_taskData.GetTaskListInRange(User.Identity.Name,model.startTime,model.endTime));
        }

        // EDIT TASK -- needs to be completed
        [Authorize]
        [HttpPatch]
        [Route("{taskId}")]
        public IActionResult EditTask([FromBody] TaskModel model,Guid taskId)
        {
            var temp = _taskData.EditTask(User.Identity.Name, model, taskId);
            return temp != null ? Ok(temp) : NotFound("Could not edit task");
        }



        // DELETE
        [Authorize]
        [HttpDelete]
        [Route("{taskId}")]
        public IActionResult DeleteTask(Guid taskId)
        {
            var temp = _taskData.DeleteTask(User.Identity.Name,taskId);
            return temp != null ? Ok(temp) : NotFound("Could not delete task");
        }
        [Authorize]
        [HttpPatch]
        [Route("complete/{taskId}")]
        public IActionResult SetComplete(Guid taskId)
        {
            var temp = _taskData.SetTaskComplete(User.Identity.Name, taskId);
            return temp != null ? Ok(temp) : NotFound("Could not edit task");
        }
        [Authorize]
        [HttpPatch]
        [Route("incomplete/{taskId}")]
        public IActionResult SetInComplete(Guid taskId)
        {
            var temp = _taskData.SetTaskInComplete(User.Identity.Name, taskId);
            return temp != null ? Ok(temp) : NotFound("Could not edit task");
        }


    }
}
