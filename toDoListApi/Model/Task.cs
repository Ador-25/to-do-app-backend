using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toDoListApi.Model
{
    public class Task
    {

        public Guid TaskId { get; set; }
        public string TaskName { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public bool isCompleted { get; set; } = false;
        public User User { get; set; }
    }
}
