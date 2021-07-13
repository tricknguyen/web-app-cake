using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Rocky_DataAcess;
using Rocky_Models;
using Rocky_DataAcess.Repository.IRepository;
using System.Security.Claims;
using Microsoft.Azure.Documents;
using Microsoft.AspNetCore.Http;

namespace Rocky.Hubs
{
    class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMessRepository _messRepo;
        public ChatHub(IHttpContextAccessor httpContextAccessor, IMessRepository messRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _messRepo = messRepo;
        }
        public Message Message { get; set; }

        public async Task SendMessage(string message)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Message = new Message()
            {
                UserName = Context.User.Identity.Name,
                UserID = userId,
                Text = message, 
                When = DateTime.Now
            };
            _messRepo.Add(Message);
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymus", message);
            _messRepo.Save();         
           
        }
        
      
    }
}
