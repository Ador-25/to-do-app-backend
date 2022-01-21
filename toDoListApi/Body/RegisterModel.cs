using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace toDoListApi.Body
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="Email Required")]
        public string Email { get; set; }
        [MinLength(6)]
        [Required(ErrorMessage = "Email Required")]
        public string Password { get; set; }
    }
}
