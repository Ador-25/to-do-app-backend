using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Model;

namespace toDoListApi.Data
{
    public interface IUserData
    {
        public User GetUser(string email);
        public User AddUser(string email);


    }
}
