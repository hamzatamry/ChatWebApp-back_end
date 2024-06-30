using ChatWeb.Core.Interfaces;
using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Interfaces.Services;
using ChatWeb.Core.Models;
using ChatWeb.Infrastructure.Repositories;

namespace ChatWeb.Infrastructure.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IUserService _userService;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public ConnectionService(IUserService userService, IConnectionRepository connectionRepository,
            IUserRepository userRepository, IMessageRepository messageRepository) 
        {
            this._userService = userService;
            this._connectionRepository = connectionRepository;
            this._userRepository = userRepository;
            this._messageRepository = messageRepository;
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

        public void DeleteConnection(string connectionId)
        {
            Connection connection = _connectionRepository.GetConnectionByConnectionId(connectionId);
            _connectionRepository.DeleteConnection(connection);
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
