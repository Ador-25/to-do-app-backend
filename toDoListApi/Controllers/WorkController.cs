using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Body;
using toDoListApi.Data;
using toDoListApi.Helper;

namespace toDoListApi.Controllers
{
    [ApiController]
    public class WorkController : ControllerBase
    {
        IWorkData _workData;
        IUserData _userData;
        public WorkController(IWorkData workData,IUserData userData)
        {
            _workData = workData;
            _userData = userData;
        }
        [HttpPost]
        [Authorize]
        [Route("api/[controller]/addwork")]
        public IActionResult AddWork([FromBody] Date date)
        {
            var userName = User.Identity.Name;
            

            if (userName == null)
                return NotFound();
            try
            {
                return Ok(_workData.AddWork(date, userName));
            }
            catch
            {
                return Ok(new Response { Status = "Error", Message = "Can not add work" });
            }
            
        }
        [HttpPatch]
        [Authorize]
        [Route("api/[controller]/editwork/{workid}")]
        public IActionResult EditWork([FromBody] Date date,Guid workid)
        {
            var userName = User.Identity.Name;
            if (userName == null)
                return Unauthorized();
            try
            {
                var work = _workData.EditWork(userName, date, workid);
               if (work!=null)
                {
                    return Ok(work);
                }
                else
                {
                    return Ok(new Response { Status = "Error", Message = "Can not Edit work" });
                }
                
            }
            catch
            {
                return Ok(new Response { Status = "Error", Message = "Can not Edit work" });
            }

        }

        [HttpGet]
        [Authorize]
        [Route("api/[controller]")]
        public IActionResult GetWorkList()
        {
            var userName = User.Identity.Name;

            if (userName == null)
                return NotFound();
            try
            {
                return Ok(_workData.GetWorkList(userName));
            }
            catch
            {
                return Ok(new Response { Status = "Error", Message = "Can not add work" });
            }

        }


        [HttpDelete]
        [Authorize]
        [Route("api/[controller]/{WorkId}")]
        public IActionResult DeleteWork(Guid WorkId)
        {
            return Ok(_workData.DeleteWork(WorkId));  

        }

    }
}
