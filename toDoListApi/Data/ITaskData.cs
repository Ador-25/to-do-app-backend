using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Body;

namespace toDoListApi.Data
{
    public interface ITaskData
    {
        List<Model.Task> GetTaskList(string email);
        List<Model.Task> GetTaskListInRange(string email,DateTimeOffset startTime, DateTimeOffset endTime);
        Model.Task AddTask(string email, TaskModel taskModel);
        Model.Task EditTask(string email, TaskModel taskModel,Guid taskId);
        Model.Task DeleteTask(string email, Guid TaskId);
        Model.Task SetTaskComplete(string email, Guid TaskId);
        Model.Task SetTaskInComplete(string email, Guid TaskId);
    }
}
