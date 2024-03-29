﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLayer;
using CommonLayer.CustomExceptions;
using DataLayer;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    public class UsersBL
    {
        public IQueryable<User> GetUsers()
        {
            return new UsersRepository().GetUsers();
        }

        public IQueryable<User> GetUsers(string keyword)
        {
            return new UsersRepository().GetUsers(keyword);
        }
        public IQueryable<User> GetUsers(string firstname, string surname)
        {
            return new UsersRepository().GetUsers(firstname, surname);
        }

        public User GetUser(string username)
        {
            return new UsersRepository().GetUser(username);
        }

        public void RegisterUser(User newUser)
        {
            UsersRepository ur = new UsersRepository();
            if (ur.DoesUsernameExist(newUser.Username))
            {
                throw new UsernameAlreadyExistsException();
            }
            else
            {
                ur.AddUser(newUser);
            }
        }

        public bool Login(string username, string code)
        {
            User u = GetUser(username);

            //generate the code based on u.Username and u.Password

            //check if u is null...throw exception
            if(code == u.Password)
            {
                return true;
            }

            return false;
        }

        public static string Encrypt(string Password, string Date)
        {
            string AllString = Password + Date;
            if (string.IsNullOrEmpty(AllString)) throw new ArgumentNullException();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(AllString);
            buffer = System.Security.Cryptography.SHA512.Create().ComputeHash(buffer);
            return Regex.Replace(Convert.ToBase64String(buffer).Substring(0, 86), "[^0-9a-zA-Z]+", ""); // strip padding and special characters
        }
    }
}
