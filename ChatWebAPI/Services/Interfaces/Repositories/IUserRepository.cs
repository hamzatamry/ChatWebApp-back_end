using ChatWeb.Core.Models;

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
