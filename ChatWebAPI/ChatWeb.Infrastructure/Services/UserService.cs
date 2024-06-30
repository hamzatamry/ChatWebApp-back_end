using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Interfaces.Services;
using ChatWeb.Core.Models;
using ChatWeb.Infrastructure.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> users = _userRepository.GetAllUsers();
            return _userRepository.GetAllUsers();
        }
    }
}
