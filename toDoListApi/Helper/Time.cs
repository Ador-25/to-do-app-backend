using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace toDoListApi.Helper
{
    public class Time
    {

        [Range(0, 23)]
        public int Hour { get; set; }
        [Range(0, 60)]
        public int Minute { get; set; }
    }
}
