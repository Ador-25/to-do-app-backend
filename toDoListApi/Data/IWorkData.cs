using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Helper;
using toDoListApi.Model;

namespace toDoListApi.Data
{
    public interface IWorkData
    {
        Work AddWork(Date date, string email);
        List<Work> GetWorkList(string email);
        Work DeleteWork(Guid WorkId);
        
    }

}
