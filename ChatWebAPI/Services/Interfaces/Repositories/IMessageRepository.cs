using ChatWeb.Core.Models;

namespace ChatWeb.Core.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        public IEnumerable<Message> GetMessagesByUserId(User user);
        public Task SaveMessageAsync(Message message);
    }
}
