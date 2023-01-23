using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAppWithSessionInAsp.Net.Controllers
{
    public class PrivateChatController : Controller
    {
        [ActionName("Index")]
        public IActionResult PrivateChatIndex()
        {
            return View("PrivateChatIndex");
        }
    }
}
