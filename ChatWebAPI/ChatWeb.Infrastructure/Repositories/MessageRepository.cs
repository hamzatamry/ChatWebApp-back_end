using ChatWeb.Core.Interfaces.Repositories;
using ChatWeb.Core.Models;
using ChatWeb.Infrastructure.Data;

namespace ChatWeb.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatWebDbContext _db;

        public MessageRepository(ChatWebDbContext context)
        {
            _db = context;
        }

        public IEnumerable<Message> GetMessagesByUserId(User user)
        {
            IEnumerable<Message> messages = _db.Messages
                                                .Where(u => u.Receiver == user)
                                                    .OrderByDescending(u => u.Id).ToList();
            return messages;
        }

        public async Task SaveMessageAsync(Message message)
        {
            _db.Messages.Add(message);
            await _db.SaveChangesAsync();
        }
    }
}
