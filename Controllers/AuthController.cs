using esabzi.Models;
using esabzi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace esabzi.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.title = "E-Sabzi";
            //checks if header have bearer token
            if (Request.Headers.ContainsKey("Authorization"))
            {
                //get token from header
                string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                if (!token.IsNullOrEmpty()) return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Login()
        {
            ViewBag.title = "E-Sabzi";
            //checks if header have bearer token
            if (Request.Headers.ContainsKey("Authorization"))
            {
                //get token from header
                string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                if (!token.IsNullOrEmpty()) return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Signup()
        {
            //checks if header have bearer token
            if (Request.Headers.ContainsKey("Authorization"))
            {
                //get token from header
                string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
                if (!token.IsNullOrEmpty()) return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            //checks if header have bearer token
            if (!Request.Headers.ContainsKey("Authorization") || Request.Headers["Authorization"].ToString().Split(" ")[1].IsNullOrEmpty())
            {
                return RedirectToAction("Login", "Auth");
            }

            //get user from cookies
            User user = CookieHelper.GetCookie<User>(Request, "user");

            //if user then send user with view otherwise reload the page

            if (user == null) return RedirectToAction("Logout", "Auth");

            return View(user);
        }

    }
}
