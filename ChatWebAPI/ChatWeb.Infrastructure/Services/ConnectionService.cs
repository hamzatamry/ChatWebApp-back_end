using ChatWeb.Core.Interfaces;
using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Interfaces.Services;
using ChatWeb.Core.Models;

namespace ChatWeb.Infrastructure.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IUserService _userService;
        private readonly IConnectionRepository _connectionRepository;

        public ConnectionService(IUserService userService, IConnectionRepository connectionRepository) 
        {
            this._userService = userService;
            this._connectionRepository = connectionRepository;
        }

        public string GetConnectionByUserId(int userID)
        {
            return _connectionRepository.GetConnectionByUserId(userID);
        }

        public IEnumerable<Connection> GetAllConnections()
        {
            return _connectionRepository.GetAllConnections();
        }

        public void AddConnection(int userId, string connectionId)
        {
            Connection connection = ConvertToConnection(userId, connectionId);
            _connectionRepository.AddConnection(connection);
        }

        public void UpdateConnection(int userId, string connectionId)
        {
            
        }

        public void deleteConnection(string connectionId)
        {

        }

        private Connection ConvertToConnection(int userId, string connectionId)
        {
            Connection c = new Connection();

            User u = _userService.GetUserById(userId);

            c.User = u;
            c.ConnectionId = connectionId;

            return c;
        }

    }
}
