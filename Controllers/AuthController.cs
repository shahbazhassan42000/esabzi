using esabzi.DB;
using esabzi.Models;
using Microsoft.AspNetCore.Mvc;

namespace esabzi.Controllers
{
    public class AuthController : Controller
    {
        private readonly EsabziContext _context;

        public AuthController()
        {
            _context = new EsabziContext();
        }

        public ViewResult Index()
        {
            return View("Login");
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

        //get all usernames
        public ActionResult Users()
        {
            List<User> users = _context.Users.Select(u => new User
            {
                Username=u.Username,
                Email=u.Email
            }).ToList();
            return Json(users);
        }

        [HttpPost]
        public ViewResult Signup(User user)
        {
            Console.WriteLine(user);
            return View();
        }

        
    }
}
