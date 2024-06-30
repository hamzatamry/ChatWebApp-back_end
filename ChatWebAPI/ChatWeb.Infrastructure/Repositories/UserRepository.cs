using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Models;
using ChatWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatWebDbContext _db;

        public UserRepository(ChatWebDbContext context)
        {
            _db = context;
        }
           
        //comming to this later : i should retrieve only userId and email
        //Another thing : i should getAllUsers() except the one who send the request

        public IEnumerable<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _db.Users.Find(id);
        }

        public User GetUserByEmail(string email)
        {
            User user = _db.Users.FirstOrDefault(u => u.Email == email);
            return user;
        }

        public void AddUser(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
        }
    }
}
