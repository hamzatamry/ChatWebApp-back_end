using ChatWeb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAllUsers();

        public User GetUserById(int id);

        public User GetUserByEmail(string email);

        public void AddUser(User user);
    }
}
