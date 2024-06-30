using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Core.Interfaces.Services
{
    public interface IUserService
    {
        public User GetUserById(int userId);

        public IEnumerable<User> GetAllUsers();
    }
}
