using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAppWithSessionInAsp.Net.Controllers
{
    //https://localhost:3000/chat
    public class ChatController : Controller
    {
        [ActionName("Index")]
        public IActionResult ChatIndex()
        {
            return View("ChatIndex");
        }
    }
}
