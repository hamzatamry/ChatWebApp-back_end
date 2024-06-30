using ChatWeb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Core.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        public IEnumerable<Message> GetMessagesByUserId(User user);
        public Task SaveMessageAsync(Message message);
    }
}
