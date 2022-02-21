using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toDoListApi.Body
{
    public class TaskModel
    {
        public string name { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        

        // might add a description later

    }
}
