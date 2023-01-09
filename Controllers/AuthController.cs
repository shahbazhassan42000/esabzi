using Microsoft.AspNetCore.Mvc;

namespace esabzi.Controllers
{
    public class AuthController : Controller
    {

        public string Index()
        {
            return "Auth Controller";
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Signup()
        {
            return View();
        }
    }
}
