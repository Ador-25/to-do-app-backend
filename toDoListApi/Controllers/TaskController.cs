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
        [Authorize]
        [HttpGet]
        public IActionResult GetSubTasks()
        {
            return Ok(_taskData.GetTaskList(User.Identity.Name));
        }
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

        [Authorize]
        [HttpGet]
        [Route("range")]
        public IActionResult GetSubTasksInRange([FromBody] RangeModel model)
        {
            return Ok(_taskData.GetTaskListInRange(User.Identity.Name,model.startTime,model.endTime));
        }
        [Authorize]
        [HttpPatch]
        [Route("{taskId}")]
        public IActionResult EditTask([FromBody] TaskModel model,Guid taskId)
        {
            var temp = _taskData.EditTask(User.Identity.Name, model, taskId);
            return temp != null ? Ok(temp) : NotFound("Could not edit task");
        }
    }
}
