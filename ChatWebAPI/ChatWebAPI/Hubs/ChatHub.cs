using ChatWeb.Api.Models;
using ChatWeb.Core.Interfaces;
using ChatWeb.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatWeb.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IConnectionService _connectionService;
        private readonly IUserService _userService;


        public ChatHub(IMessageService messageService, IConnectionService connectionService,
           IUserService userService)
        {
            _messageService = messageService;
            _connectionService = connectionService;
            _userService = userService;
        }

        public void AddConnection([FromBody] ConnectionModel connectionModel)
        {
            _connectionService.AddConnection(connectionModel.UserId, connectionModel.ConnectionId);
        }

        public void DeleteConnection(string connectionId)
        {
            _connectionService.DeleteConnection(connectionId);
        }

        public async Task AddMessage(int senderId, int receiverId, string message)
        {
            var receiverConnectionId = _connectionService.GetConnectionByUserId(receiverId);

            _messageService.AddMessage(senderId, receiverId, message);

            //if the user is connected
            if (receiverConnectionId != null)
            {
                string senderEmail = _userService.GetUserById(senderId).Email;

                // Send message directly to the receiver
                await Clients.Client(receiverConnectionId).SendAsync("ReceiveMessage", senderEmail, message);
            }
            else //if the user is not connected
            {
                //i should add some sort of message Queing service for non logged in users

            }       
        }
  
    }
}
