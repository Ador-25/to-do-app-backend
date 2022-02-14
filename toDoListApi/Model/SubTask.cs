using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Helper;

namespace toDoListApi.Model
{
    public class SubTask
    {
        [Key]
        public Guid TaskId { get; set; }
        [Required]
        public string TaskName { get; set; }

        // 0 represnets incomplete
        // 1 represents complete
        [Range(0,1)]
        public int IsCompleted { get; set; }

        public Work Work { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan  EndTime { get; set; }



    }
}
