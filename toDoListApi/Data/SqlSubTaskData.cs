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

        public SubTask AddSubTask(Time startTime, Time endTime, string name, Guid workid)
        {
            try
            {

                SubTask subTask = new SubTask();
                subTask.StartTime = new TimeSpan(startTime.Hour, startTime.Minute, 0);
                subTask.EndTime = new TimeSpan(endTime.Hour, endTime.Minute, 0);
                subTask.TaskId = new Guid();
                subTask.TaskName = name;
                var work = _userDbContext.Work.Find(workid);
                subTask.Work = work;
                _userDbContext.SubTask.Add(subTask);
                _userDbContext.SaveChanges();
                
                return subTask;
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

        private bool isTimeAvailable(Guid workid)
        {
            List<SubTask> list = _userDbContext.SubTask.OrderBy(s => s.StartTime) //remove order by if problem
                .Where(u => u.Work.WorkId == workid)
                .ToList();

            // get a sorted list
            // case 1: given time is between two times
            // check if giventime.start >=elem[i].end && giventime.end <=elem[i+1].start ====> then insert
            //case 2: given time is the first ***********
            // check if giventime.end > elem[first].start =>>>>>> insert
            // case 3: given time is last
            // check if giventime.start >= elem[last].end =>>>>>> insert 
            // if array length==0                         =>>>>>> insert

            foreach(SubTask temp in list)
            {
                // givenTime = some range
                // list= {0-2, 3-4, 4.40-6 , 7-8.30 , 9-11  }
                // if givenTime.start >= elem.start && elem.end > givenTime.start

            }


            return false;
        }

        // TEST IT***********************************
        // GET A SORTED LIST
        private static bool isSlotAvailable(List<SubTask> list, SubTask givenTime)
        {

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
    }
}
