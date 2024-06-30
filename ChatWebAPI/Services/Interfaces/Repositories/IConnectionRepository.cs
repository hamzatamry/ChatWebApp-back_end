using ChatWeb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
