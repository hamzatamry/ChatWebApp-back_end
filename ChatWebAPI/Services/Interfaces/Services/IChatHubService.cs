using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Core.Interfaces.Services
{
    public interface IChatHubService
    {
        public Task SendMessage(int senderId, int receiverId, string message);
    }
}
