using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Interfaces.Services;
using ChatWeb.Core.Models;

namespace ChatWeb.Infrastructure.Services
{
    public class ChatHubService : IChatHubService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        public ChatHubService(IUserRepository userRepository, IMessageRepository messageRepository)
        {
            this._userRepository = userRepository;
            this._messageRepository = messageRepository;
        }

        public async Task SendMessage(int senderId, int receiverId, string message)
        {
            //retrive both senderUser and receiver user
            User senderUser = _userRepository.GetUserById(senderId);
            User receiverUser = _userRepository.GetUserById(receiverId);

            var newMessage = new Message
            {
                Sender = senderUser,
                Receiver = receiverUser,
                Content = message,
                SentAt = DateTime.Now
            };

            _messageRepository.SaveMessageAsync(newMessage);
        }
    }
}
