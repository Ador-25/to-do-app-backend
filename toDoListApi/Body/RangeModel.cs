using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace toDoListApi.Body
{
    public class RangeModel
    {
        public DateTimeOffset startTime { get; set; }
        public DateTimeOffset endTime { get; set; }
    }
}
