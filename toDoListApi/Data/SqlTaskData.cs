using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Body;
using toDoListApi.Connection;

namespace toDoListApi.Data
{
    public class SqlTaskData : ITaskData
    {
        ApplicationDbContext _userDbContext;
        public SqlTaskData(ApplicationDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public Model.Task AddTask(string email, TaskModel taskModel)
        {
            var user = _userDbContext.User.Find(email);
            if (isTimeAvailable(email, taskModel) && user != null)
            {
                Model.Task task = new Model.Task();
                task.TaskId = new Guid();
                task.TaskName = taskModel.name;
                task.StartTime = taskModel.StartTime;
                task.EndTime = taskModel.EndTime;
                task.User = user;
                task.User.Email = email;
                _userDbContext.Task.Add(task);  // recheck
                _userDbContext.SaveChanges();
                return task;
            }
            else return null;
        }


        // fix edit

        public Model.Task EditTask(string email, TaskModel taskModel,Guid taskId)
        {
            var user = _userDbContext.User.Find(email);
            var task= _userDbContext.Task.Find(taskId);

            if (EditValidation(email,taskId,taskModel) && user != null && task!=null)
            {
                task.TaskName = taskModel.name;
                task.StartTime = taskModel.StartTime;
                task.EndTime = taskModel.EndTime;
                _userDbContext.Task.Update(task);
                _userDbContext.SaveChanges();
                return task;
            }
            else return null;
        }


        public bool EditValidation(string email,Guid taskId,TaskModel taskModel)
        {
            var temp = _userDbContext.Task.Find(taskId);
            List<Model.Task> list = GetTaskList(email);
            int count = 0;
            foreach(Model.Task flag in list)
            {
                count++;
                if (flag.TaskId == taskId)
                {
                    break;
                }
            }
            list.RemoveAt(count);
            if (list.Count == 0)
                return true;
            if (taskModel.EndTime < list.ElementAt(0).StartTime)
            {
                return true;
            }
            if (list.ElementAt(list.Count - 1).EndTime < taskModel.StartTime)
            {
                return true;
            }
            for (int i = 0; i < list.Count - 1; i++)
            {
                //Console.WriteLine("CAME HERE");
                Model.Task temm = list.ElementAt(i);
                Model.Task next = list.ElementAt(i + 1);
                if (taskModel.StartTime >= temm.EndTime && next.StartTime >= taskModel.EndTime)
                    return true;
            }
            return false;
        }

        public List<Model.Task> GetTaskList(string email)
        {
            var user = _userDbContext.User.Find(email);
            return _userDbContext.Task
                .Where(u => u.User.Email==user.Email)
                .OrderBy(t => t.StartTime)
                .ToList();
        }
        private bool isTimeAvailable(string email,TaskModel taskModel)
        {
            List<Model.Task> list = GetTaskList(email);
            if (list.Count == 0)
                return true;
            if(taskModel.EndTime< list.ElementAt(0).StartTime)
            {
                return true;
            }
            if (list.ElementAt(list.Count - 1).EndTime < taskModel.StartTime) 
            {
                return true; 
            }
            for (int i = 0; i < list.Count - 1; i++)
            {
                //Console.WriteLine("CAME HERE");
                Model.Task temp = list.ElementAt(i);
                Model.Task next = list.ElementAt(i + 1);
                if (taskModel.StartTime >= temp.EndTime && next.StartTime >= taskModel.EndTime)
                    return true;
            }
            return false;
        }
         
        //test for clashing dates
        public List<Model.Task> GetTaskListInRange(string email,DateTimeOffset startTime, DateTimeOffset endTime)
        {
            List<Model.Task> list = GetTaskList(email);
            List<Model.Task> newList = new List<Model.Task>();
            foreach (Model.Task temp in list)
            {
                if(temp.StartTime>= startTime && temp.StartTime<= endTime)
                {
                    newList.Add(temp);
                    continue;
                }
                else if (temp.EndTime >= startTime && temp.EndTime <=endTime)
                {
                    newList.Add(temp);
                }
            }
            return newList;
         }

        public Model.Task DeleteTask(string email, Guid TaskId) {
            var user = _userDbContext.User.Find(email);
            var temp = _userDbContext.Task.Find(TaskId);
            if (temp.User== user)
            {
                _userDbContext.Task.Remove(temp);
                _userDbContext.SaveChanges();
                return temp;
            }
            return null;
        }

        public Model.Task SetTaskComplete(string email, Guid TaskId)
        {
            var user = _userDbContext.User.Find(email);
            var temp = _userDbContext.Task.Find(TaskId);
            if (temp.User == user)
            {
                temp.isCompleted = true;
                _userDbContext.Task.Update(temp);
                _userDbContext.SaveChanges();
                return temp;
            }
            return null;
        }

        public Model.Task SetTaskInComplete(string email, Guid TaskId)
        {
            var user = _userDbContext.User.Find(email);
            var temp = _userDbContext.Task.Find(TaskId);
            if (temp.User == user)
            {
                temp.isCompleted = false;
                _userDbContext.Task.Update(temp);
                _userDbContext.SaveChanges();
                return temp;
            }
            return null;
        }
    }
}
