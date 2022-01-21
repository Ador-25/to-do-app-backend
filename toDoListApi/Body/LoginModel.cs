using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace toDoListApi.Body
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email Needed")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Needed")]
        public string Password { get; set; }
    }
}
