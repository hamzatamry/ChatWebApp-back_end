using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Interfaces.Services;
using ChatWeb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<Message> GetMessagesByUserId(int userId)
        {
            User user = _userRepository.GetUserById(userId);

            return _messageRepository.GetMessagesByUserId(user);
        }
    }
}
