using esabzi.DB;
using esabzi.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace esabzi.Controllers
{
    public class AuthController : Controller
    {
        private readonly EsabziContext _context;
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _context = new EsabziContext();
            _config = config;
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

        //signup route
        [HttpPost]
        public ActionResult Users([FromBody] User user)
        {
            if (user.ValidateSignup())
            {
                //encrypt password
                user.EncryptPassword();
                _context.Users.Add(user);
                _context.SaveChanges();
                return StatusCode((int)HttpStatusCode.OK, "User added successfully");
            }
            else
            {
                //send error 422 with a message
                return StatusCode((int)HttpStatusCode.UnprocessableEntity, "Please fill all the fields");
            }
        }

        //login route
        [Route("auth/users/login")]
        [HttpPost]
        public ActionResult Login([FromBody] User user)
        {
            if (user.ValidateLogin())
            {
                //check if user exists
                User? userExists = _context.Users.Where(u => u.Username == user.Username).FirstOrDefault();
                if (userExists != null)
                {
                    //compare password
                    if (userExists.ComparePassword(user.Password))
                    {
                        //Generate JWT Token
                        JwtSecurityToken JWT = user.GenerateJWT(_config);
                        return StatusCode((int)HttpStatusCode.OK,new {token=new JwtSecurityTokenHandler().WriteToken(JWT)});
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.Unauthorized, Json(new { status = "error", message = "Incorrect password" }));
                    }
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.Unauthorized, Json(new { status = "error", message = "User does not exist" }));
                }
            }
            else
            {
                //send error 422 with a message
                return StatusCode((int)HttpStatusCode.UnprocessableEntity, "Please fill all the fields");
            }
        }


    }
}
