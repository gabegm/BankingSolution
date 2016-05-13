using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLayer;

namespace DataLayer
{
    public class UsersRepository : ConnectionClass
    {
        public UsersRepository() : base() { }

        public IQueryable<User> GetUsers()
        {
            return Entity.Users;

            //return from u in Entity.Users
            //       select u;
        }

        public IQueryable<User> GetUsers(string keyword)
        {
            return Entity.Users.Where(u => u.Username.Contains(keyword) || u.FirstName.Contains(keyword)
                || u.Surname.Contains(keyword) || u.Email.Contains(keyword));

            //return from u in Entity.Users
            //       where u.FirstName.Contains(keyword) 
            //       select u;

        }

        public IQueryable<User> GetUsers(string firstname, string surname)
        {
            return Entity.Users.Where(u => u.FirstName.Contains(firstname)
                && u.Surname.Contains(surname));

            //return from u in Entity.Users
            //       where u.FirstName.Contains(keyword) 
            //       select u;

        }

        public User GetUser(string username)
        {
            return Entity.Users.SingleOrDefault(x => x.Username == username);
        }

        public void AddUser(User u)
        {
            Entity.Users.Add(u);
            Entity.SaveChanges();
        }

        public bool DoesUsernameExist(string username)
        {
            return Entity.Users.Count(x => x.Username == username) > 0 ? true : false;

            //if (GetUser(username) == null)
            //    return false;
            //else return true;

        }
    }
}
