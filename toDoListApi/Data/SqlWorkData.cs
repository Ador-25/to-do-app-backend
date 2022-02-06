using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Connection;
using toDoListApi.Helper;
using toDoListApi.Model;

namespace toDoListApi.Data
{
    public class SqlWorkData:IWorkData
    {
        ApplicationDbContext _userDbContext;
        public SqlWorkData(ApplicationDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }



        public Work AddWork(Date date, string email)
        {
            var user = _userDbContext.User.Find(email);
            if (user == null)
                throw new NotImplementedException();
            else
            {
                List<Work> list = GetWorkList(email);
                foreach(Work temp in list)
                {
                    if(temp.Date.Year== date.Year && temp.Date.Month==date.Month && temp.Date.Day == date.Day)
                    {
                        return null;
                    }
                }
                Work work = new Work();
                work.WorkId = new Guid();
                work.User = user;
                work.User.Email = user.Email;
                DateTime dateTime = new DateTime(date.Year,date.Month,date.Day);
                work.Date = dateTime;
                _userDbContext.Work.Add(work);
                _userDbContext.SaveChanges();
                return work;
            }
        }

        public Work DeleteWork(Guid WorkId)
        {
            var work = _userDbContext.Work.Find(WorkId);
            if (work != null)
            {
                _userDbContext.Remove(work);
                _userDbContext.SaveChanges();
                return work;
            }
            else
                return work;
        }

        public List<Work> GetWorkList(string email)
        {
            return _userDbContext.Work
                .Where(w => w.User.Email == email)
                .ToList();
        }
      
    }
}
