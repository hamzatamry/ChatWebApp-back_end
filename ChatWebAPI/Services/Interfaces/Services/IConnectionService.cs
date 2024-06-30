using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Core.Interfaces
{
    public interface IConnectionService
    {
        public string GetConnectionByUserId(int userID);

        public IEnumerable<Connection> GetAllConnections();

        public void AddConnection(int userId, string connectionId);
        public void UpdateConnection(int userId, string connectionId);

        public void deleteConnection(string connectionId);
    }
}
