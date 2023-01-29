using esabzi.DB;
using esabzi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    }
}
