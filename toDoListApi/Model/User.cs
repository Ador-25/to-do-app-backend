using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace toDoListApi.Model
{
    public class User
    {
        [Key]
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
