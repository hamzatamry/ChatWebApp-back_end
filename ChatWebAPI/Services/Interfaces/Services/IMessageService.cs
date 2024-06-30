using ChatWeb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Core.Interfaces.Services
{
    public interface IMessageService
    {
        public IEnumerable<Message> GetMessagesByUserId(int userId);
    }
}
