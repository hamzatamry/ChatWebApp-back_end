using ChatWeb.Core.Models;

namespace ChatWeb.Core.Interfaces.Repositories
{
    public interface IConnectionRepository
    {
        public IEnumerable<Connection> GetAllConnections();

        public string GetConnectionByUserId(int userId);

        public void AddConnection(Connection connection);

        public void UpdateConnection(Connection connection);

        public void DeleteConnection(Connection connection);
    }
}
