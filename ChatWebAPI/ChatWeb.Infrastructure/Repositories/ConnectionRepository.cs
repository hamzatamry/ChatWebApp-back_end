using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Models;
using ChatWeb.Infrastructure.Data;

namespace ChatWeb.Infrastructure.Repositories
{
    public class ConnectionRepository : IConnectionRepository
    {
        private readonly ChatWebDbContext _db;

        public ConnectionRepository(ChatWebDbContext context) 
        { 
            _db = context;
        }

        public IEnumerable<Connection> GetAllConnections()
        {
            IEnumerable<Connection> connections = _db.Connections.ToList();
            return connections;
        }

        public string GetConnectionByUserId(int userID)
        {
            User user = _db.Users.Find(userID);

            return _db.Connections
                    .Where(u => u.User == user)
                        .OrderByDescending(c => c.ConnectedAt)
                            .Select(c => c.ConnectionId)
                                .FirstOrDefault();
        }

        public Connection GetConnectionByConnectionId(string connectionId)
        {
            return _db.Connections
                    .Where(c => c.ConnectionId == connectionId)
                        .OrderByDescending(c => c.ConnectedAt)
                                .FirstOrDefault();
        }

        public void AddConnection(Connection connection)
        {
            _db.Connections.Add(connection);
            _db.SaveChanges();
        }
        public void UpdateConnection(Connection connection)
        {
            //completing it after
            //_db.Connections.Update().
        }

        public void DeleteConnection(Connection connection)
        {
            _db.Connections.Remove(connection);
            _db.SaveChanges();
        }
    }
}
