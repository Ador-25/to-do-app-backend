using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace toDoListApi.Model
{
    public class Work
    {
        [Key]
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }
        public User User { get; set; }

        
    }
}
