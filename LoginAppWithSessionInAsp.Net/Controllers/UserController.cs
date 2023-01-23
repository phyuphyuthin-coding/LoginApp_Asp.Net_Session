using LoginAppWithSessionInAsp.Net.Models.DataModels;
using LoginAppWithSessionInAsp.Net.Models.ViewModels;
using LoginAppWithSessionInAsp.Net.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAppWithSessionInAsp.Net.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserViewModel userViewModel)
        {
            string message = "";
            if (userViewModel.Email == null ||
                string.IsNullOrEmpty(userViewModel.Email) ||
                EmailValidation.EmailValidator.Validate(userViewModel.Email))
            {
                message = "Invalid Email!";
                goto result;
            }

            var isExist = userService.GetUserByEmail(userViewModel.Email);
            if (isExist)
            {
                message = "Email already exists!";
                goto result;
            }

            userService.CreateUser(userViewModel);
            ViewBag.msg = "Successfully registered!";
            return View("Login");
        result:
            ViewBag.message = message;
            return View("Register");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult WelcomePage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserViewModel userViewModel)
        {
            string errMsg = "";
            if (userViewModel.Email == null || userViewModel.Password == null)
            {
                errMsg = "Invalid Email or Password.";
                goto result;
            }
            UserDataModel user = userService.ExistByEmailAndPassword(userViewModel.Email, userViewModel.Password);
            if (user == null)
            {
                errMsg = "Invalid Email or Password.";
                goto result;
            }
            HttpContext.Session.SetString("userName", user.userName);
            HttpContext.Session.SetString("userEmail", user.email);

            //ViewBag.Name = HttpContext.Session.GetString("userName");
            //ViewBag.Email = HttpContext.Session.GetString("userEmail");
            return RedirectToAction("WelcomePage");
        result:
            ViewBag.errMsg = errMsg;
            return View("Login");
        }
    }
}
