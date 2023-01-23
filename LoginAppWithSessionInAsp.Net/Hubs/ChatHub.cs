using LoginAppWithSessionInAsp.Net.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAppWithSessionInAsp.Net.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _db;

        public ChatHub(AppDbContext db)
        {
            _db = db;
        }

        public static List<SignalRUserModel> lstSignalRUser { get; set; } = new List<SignalRUserModel>();

        public async Task ServerSendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ClientReceiveMessage", user, message);
        }

        public async Task ServerPrivateSendMessage(string user, string toUserId, string message)
        {
            var item = lstSignalRUser.FirstOrDefault(x => x.UserName == toUserId);
            if (item == null) return;

            await Clients.Client(item.ConnectionId).SendAsync("ClientPrivateReceiveMessage", user, message);
        }

        public void ServerAddConnection(string userName)
        {
            lstSignalRUser = lstSignalRUser ?? new List<SignalRUserModel>();
            var item = lstSignalRUser.FirstOrDefault(x => x.UserName == userName);
            if (item != null) lstSignalRUser.Remove(item);
            lstSignalRUser.Add(new SignalRUserModel
            {
                ConnectionId = Context.ConnectionId,
                UserName = userName
            });
        }

        public async Task ServerNotificationCount()
        {
            var count = _db.Blogs.Count();
            await Clients.All.SendAsync("ClientReceiveNotificationCount", count);
        }
    }

    public class SignalRUserModel
    {
        public string UserName { get; set; }
        public string ConnectionId { get; set; }
    }
}
