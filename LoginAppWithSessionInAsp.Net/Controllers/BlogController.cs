using LoginAppWithSessionInAsp.Net.Hubs;
using LoginAppWithSessionInAsp.Net.Models;
using LoginAppWithSessionInAsp.Net.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAppWithSessionInAsp.Net.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHubContext<ChatHub> _hubContext;

        public BlogController(AppDbContext db, IHubContext<ChatHub> hubContext)
        {
            _db = db;
            _hubContext = hubContext;
        }

        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
            return View("BlogIndex");
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> CreateBlogAsync(BlogDataModel blogDataModel)
        {
            _db.Blogs.Add(blogDataModel);
            _db.SaveChanges();

            var count = _db.Blogs.Count();
            await _hubContext.Clients.All.SendAsync("ClientReceiveNotificationCount", count);
            return View("BlogIndex");
        }

        [ActionName("List")]
        public IActionResult BlogList()
        {
            var lst = _db.Blogs.OrderByDescending(x => x.Blog_Id).ToList();
            return View("BlogList", lst);
        }
    }
}
