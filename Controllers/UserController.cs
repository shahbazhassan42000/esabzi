using esabzi.Models;
using esabzi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace esabzi.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            User user = CookieHelper.GetCookie<User>(Request, "user");
            return View(user);
        }
    }
}
