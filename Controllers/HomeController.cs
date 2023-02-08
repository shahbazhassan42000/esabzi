using esabzi.Models;
using esabzi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace esabzi.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ViewResult Index()
        {
            ViewBag.title = "E-Sabzi";
            //get user from cookies
            User user=CookieHelper.GetCookie<User>(Request,"user");
            ViewBag.user = user;
            return View(user);
        }
      
    }
}