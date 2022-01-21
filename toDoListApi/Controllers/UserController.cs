using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Data;

namespace toDoListApi.Controllers
{
    [ApiController]
    public class UserController:ControllerBase
    {
        IUserData _userData;
        public UserController(IUserData userData)
        {
            _userData = userData;
        }
        [HttpGet]
        [Authorize]
        [Route("api/[controller]/user")]
        public IActionResult GetUser()
        {
            var userName = User.Identity.Name;
            if (userName == null)
                return NotFound();
            return Ok(_userData.GetUser(userName));
        }

    }
}
