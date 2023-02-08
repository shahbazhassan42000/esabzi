﻿using esabzi.DB;
using esabzi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using esabzi.Models.Repositories;
using esabzi.Utils;

namespace esabzi.Controllers
{
    [Route("api/user/")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private MainRepository repository;
        private readonly EsabziContext _context;
        private readonly IConfiguration _config;

        public UserAPIController(IConfiguration config)
        {
            _context = new EsabziContext();
            _config = config;
            repository = new MainRepository(_context, _config);
        }


        //get all usernames
        [AllowAnonymous]
        [HttpGet("Usernames")]
        public ActionResult Usernames()
        {
            //get all users minimal information
            List<User> users = repository.getAllUsernames();
            return Ok(users);
        }

        //get all users
        [Authorize(Roles ="ADMIN")] 
        [HttpGet("Users")]
        public ActionResult Users()
        {
            List<User> users = repository.getAllUsers();
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
                    repository.signup(user);
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
                User? userExists = repository.getUserByUsername(user);
                if (userExists != null)
                {
                    //compare password
                    if (userExists.ComparePassword(user.Password))
                    {
                        //Generate JWT Token
                        string JWT = GenerateJWT(userExists);
                        //remove user password and set in cookies
                        userExists.Password = "";
                        //set user object in cookies
                        CookieHelper.SetCookie(Response, "user", userExists);
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

        //get user from claims
        public User GetUserFromClaims(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.ReadToken(token) as JwtSecurityToken;
                var usernameClaim = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var roleClaim = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                if (usernameClaim != null && roleClaim != null)
                {
                    return new User
                    {
                        Username = usernameClaim.Value,
                        Role = roleClaim.Value
                    };
                }

            }
            return new User();
        }

    }
}