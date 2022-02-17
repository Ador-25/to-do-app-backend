using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Connection;
using toDoListApi.Helper;
using toDoListApi.Model;

namespace toDoListApi.Data
{
    public class SqlSubTaskData : ISubTaskData
    {

        ApplicationDbContext _userDbContext;
        public SqlSubTaskData(ApplicationDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public SubTask AddSubTask(Time startTime, Time endTime, string name, Guid workid,string email)
        {
            try
            {
                List<SubTask> createdList = GetSubtasks(workid, email);
                
                    SubTask subTask = new SubTask();
                    subTask.StartTime = new TimeSpan(startTime.Hour, startTime.Minute, 0);
                    subTask.EndTime = new TimeSpan(endTime.Hour, endTime.Minute, 0);
                    subTask.TaskId = new Guid();
                    subTask.TaskName = name;
                    var work = _userDbContext.Work.Find(workid);
                    subTask.Work = work;
                    if (isSlotAvailable(createdList, subTask)) // working perfectly if one task ends at 17.30 other has to start from 17.31
                       {
                            _userDbContext.SubTask.Add(subTask);
                            _userDbContext.SaveChanges();
                            return subTask;
                        }
                else
                {
                    return null;
                }         
                
            }
            catch
            {
                return null;
            }
        }

        public List<SubTask> GetSubtasks(Guid workid, string email)
        {
            return _userDbContext.SubTask
                .Where(u => u.Work.User.Email == email && u.Work.WorkId== workid).ToList();

        }
        // to be completed

       

        // TEST IT***********************************
     
        private static bool isSlotAvailable(List<SubTask> list, SubTask givenTime)
        {
            list = list.OrderBy(l => l.StartTime).ToList(); // sorting the list based on start time
            // if no tasks available
            if (list.Count == 0)
                return true;
            // check if first element
            if (givenTime.EndTime < list.ElementAt(0).StartTime)
                return true;
            // check last
            if (givenTime.StartTime > list.ElementAt(list.Count - 1).EndTime)
                return true;
            int count = 0;
            for (int i = 0; i < list.Count - 1; i++)
            {
                //Console.WriteLine("CAME HERE");
                SubTask temp = list.ElementAt(i);
                SubTask next = list.ElementAt(i + 1);
                if (givenTime.StartTime >= temp.EndTime && next.StartTime >= givenTime.EndTime)
                    return true;


            }
            return false;
        }
        public SubTask DeleteSubTask(string email, Guid taskId)
        {
            var temp = _userDbContext.SubTask.Find(taskId);
            if (temp != null)
            {
                _userDbContext.SubTask.Remove(temp);
                _userDbContext.SaveChanges();
                return temp;
            }
            else return null;
        }
        // implement later to check if subtask belongs to user
        private bool userHasSubTask()
        {
            return true;
        }

        public SubTask EditSubTask(Time startTime, Time endTime, string name, Guid taskid)
        {
            var subTask = _userDbContext.SubTask.Find(taskid);// this is taskid
            if (subTask != null)
            {
                subTask.StartTime = new TimeSpan(startTime.Hour, startTime.Minute, 0);
                subTask.EndTime = new TimeSpan(endTime.Hour, endTime.Minute, 0);
                subTask.TaskName = name;
                _userDbContext.SubTask.Update(subTask);
                _userDbContext.SaveChanges();
            }
            return subTask;
        }

        public SubTask MakeComplete(Guid taskId)
        {
            var subTask = _userDbContext.SubTask.Find(taskId);
            if (subTask!= null)
            {
                subTask.IsCompleted = 1;
                _userDbContext.SubTask.Update(subTask);
                _userDbContext.SaveChanges();
                return subTask;
            }
            else
                return null;
        }

        public SubTask MakeInComplete(Guid taskId)
        {
            var subTask = _userDbContext.SubTask.Find(taskId);
            if (subTask != null)
            {
                subTask.IsCompleted = 0;
                _userDbContext.SubTask.Update(subTask);
                _userDbContext.SaveChanges();
                return subTask;
            }
            else
                return null;
        }

        public string NumberOfSubTaskDoneInWork(Guid workid, string email)
        {
            List<SubTask> list = GetSubtasks(workid, email);
            int count = 0;
            foreach(SubTask temp in list)
            {
                if (temp.IsCompleted == 1)
                    count++;
            }
            string dum = (list.Count == 1 || list.Count == 0) ? " task" : " tasks";
            return (count+ " out of " + list.Count+dum+ " completed");
        }
    }
}
