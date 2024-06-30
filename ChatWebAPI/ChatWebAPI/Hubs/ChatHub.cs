using ChatWeb.Core.Interfaces;
using ChatWeb.Core.Interfaces.Services;
using ChatWeb.Core.Models;
using ChatWeb.Infrastructure.Data;
using ChatWeb.Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ChatWeb.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatHubService _chatHubService;
        private readonly IConnectionService _connectionService;
        private readonly IUserService _userService;


        public ChatHub(IChatHubService chatHubService, IConnectionService connectionService,
           IUserService userService)
        {
            _chatHubService = chatHubService;
            _connectionService = connectionService;
            _userService = userService;
        }


        public async Task SendMessage(int senderId, int receiverId, string message)
        {
            var receiverConnectionId = _connectionService.GetConnectionByUserId(receiverId);

            _chatHubService.SendMessage(senderId, receiverId, message);

            //if the user is connected
            if (receiverConnectionId != null)
            {
                string senderEmail = _userService.GetUserById(senderId).Email;

                // Send message directly to the receiver
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", senderEmail, message);
            }
            else //if the user is not connected
            {
                //i should add A messageQueing service for non logged in users

            }       
        }
    }
}
