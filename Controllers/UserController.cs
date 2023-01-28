using esabzi.DB;
using esabzi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace esabzi.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EsabziContext _context;
        private readonly IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _context = new EsabziContext();
            _config = config;
        }

        //get all usernames
        [AllowAnonymous]
        [HttpGet("Usernames")]
        public ActionResult Usernames()
        {
            List<User> users = _context.Users.Select(u => new User
            {
                Username = u.Username,
                Email = u.Email
            }).ToList();
            return Ok(users);
        }

        //get all usernames
        [Authorize(Roles ="ADMIN")] 
        [HttpGet("Users")]
        public ActionResult Users()
        {
            List<User> users = _context.Users.ToList();
            return Ok(users);
        }

        //signup route
        [AllowAnonymous]
        [HttpPost("Users")]
        public ActionResult Users([FromBody] User user)
        {
            if (user.ValidateSignup())
            {
                //encrypt password
                user.EncryptPassword();
                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return Ok("User added successfully");
                }catch(Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                //send error 422 with a message
                return UnprocessableEntity("Please fill all the fields");
            }
        }

        ////login route
        [AllowAnonymous]
        [HttpPost("Login")]
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
                        string JWT = GenerateJWT(userExists);
                        //return json "{"token":JWT}
                        //set token in cookies
                        Response.Cookies.Append("token", JWT);
                        //add token in header as bearear token
                        
                        
                        return Ok(new { token = JWT });
                    }
                    else
                    {
                        return NotFound("Invalid Password");
                    }
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            else
            {
                //send error 422 with a message
                return UnprocessableEntity("Please fill all the fields");
            }
        }


        //generate JWT token
        public string GenerateJWT(User user)
        {
            //creating claims to store in token
            Claim[] claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

            //creating security key
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            //creating signed credentials
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //generating token
            JwtSecurityToken token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMonths(1),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
