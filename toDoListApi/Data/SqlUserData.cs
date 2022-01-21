using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toDoListApi.Connection;
using toDoListApi.Model;

namespace toDoListApi.Data
{
    public class SqlUserData : IUserData
    {
        ApplicationDbContext _userDbContext;
        public SqlUserData(ApplicationDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public User AddUser(string email)
        {
            var temp = _userDbContext.User.Find(email);
            if (temp == null)
            {
                User user = new User();
                user.Email = email;
                _userDbContext.User.Add(user);
                _userDbContext.SaveChanges();
                return user;
            }
            else
                return null;
            
        }

        public User GetUser(string email)
        {
            var user = _userDbContext.User.Find(email);
            return user != null ? user : null;
        }
    }
}
