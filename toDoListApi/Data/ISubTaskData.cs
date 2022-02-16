using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Helper;
using toDoListApi.Model;

namespace toDoListApi.Data
{
    public interface ISubTaskData
    {
        List<SubTask> GetSubtasks(Guid workid,string email);
        SubTask AddSubTask(Time startTime, Time endTime, string name, Guid workid,string email);
        SubTask DeleteSubTask(string email, Guid taskId);
        SubTask EditSubTask(Time startTime, Time endTime, string name, Guid workid);

    }
}
