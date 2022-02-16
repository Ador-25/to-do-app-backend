using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Data;
using toDoListApi.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace toDoListApi.Controllers
{

    [ApiController]
    public class SubTaskController : ControllerBase
    {
        IWorkData _workData;
        ISubTaskData _subTaskData;
        public SubTaskController(IWorkData workData, ISubTaskData subTaskData)
        {
            _workData = workData;
            _subTaskData = subTaskData;
        }


        //*****************GET ALL TASKS IN A WORK*******************
        [Authorize]
        [HttpGet]
        [Route("api/[controller]/{workid}")]
        public IActionResult GetSubTasks(Guid workId)
        {
            var user = User.Identity.Name;
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(_subTaskData.GetSubtasks(workId,user));
        }


        //****************ADD A SUBTASK************

        // get ordered list
        [Authorize]
        [HttpPost]
        [Route("api/[controller]/{name}/{workid}")]
        public IActionResult AddSubTask([FromBody] SubTaskBody subTask,string name,Guid workid)
        {
            var user = User.Identity.Name;
            if (user != null)
            {
                return Ok(_subTaskData.AddSubTask(subTask.StartTime, subTask.EndTime, name, workid,user));
            }
            return Unauthorized("Can not add task");
           
            
        }
        [Authorize]
        [HttpDelete]
        [Route("api/[controller]/{taskid}")]
        public IActionResult DeleteSubTask(Guid taskid)
        {
            var user = User.Identity.Name;
            if (user != null)
            {
                return Ok(_subTaskData.DeleteSubTask(user,taskid));
            }
            return Unauthorized("Can not delete task");


        }

        // ADD Slot Validation for edit
        [Authorize]
        [HttpPatch]
        [Route("api/[controller]/{name}/{taskid}")]
        public IActionResult EditSubTask([FromBody] SubTaskBody subTask, string name, Guid taskid)
        {
            var user = User.Identity.Name;
            if (user != null)
            {
                return Ok(_subTaskData.EditSubTask(subTask.StartTime,subTask.EndTime,name,taskid));
            }
            return Unauthorized("Can not delete task");


        }
    }
}
