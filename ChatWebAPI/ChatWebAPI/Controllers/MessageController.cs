using ChatWeb.Core.Interfaces.Services;
using ChatWeb.Core.Models;
using Microsoft.AspNetCore.Mvc;
using ChatWeb.Api.Models;

namespace ChatWeb.Api.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public MessageController(IMessageService messageService, IUserService userService) 
        {
            _messageService = messageService;
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public IActionResult getMessagesByUserId(int userId)
        {
            IEnumerable<Message> messages = _messageService.GetMessagesByUserId(userId);

            IEnumerable<MessageModel> messageModels = ConvertFromMessageToMessageModel(messages);

               
            return Ok(messageModels);
        }

        private IEnumerable<MessageModel> ConvertFromMessageToMessageModel(IEnumerable<Message> messages)
        {
            List<MessageModel> messageModels = new List<MessageModel>();

            foreach (var message in messages)
            {
                string senderEmail = _userService.GetUserById(message.SenderId).Email;

                MessageModel messageModel = new MessageModel();

                messageModel.SenderEmail = senderEmail;
                messageModel.Message = message.Content;

                messageModels.Add(messageModel);

            }

            return messageModels;
        }



    }
}
