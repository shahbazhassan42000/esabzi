using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using bcrypt = BCrypt.Net.BCrypt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;

namespace esabzi.Models;

public partial class User
{
    //encrypt password
    public void EncryptPassword()
    {
        string salt = bcrypt.GenerateSalt();
        Password = bcrypt.HashPassword(Password, salt);
    }

    //compare password
    public bool ComparePassword(string password)
    {
        return bcrypt.Verify(password, Password);
    }

    //validate signup credentials
    public bool ValidateSignup()
    {
        return Name != null && Email != null && ContactNo != null && Username != null && Password != null && Address != null;
    }

    //validate login credentials
    public bool ValidateLogin()
    {
        return Username != null && Password != null;
    }

    //generate JWT token
    public JwtSecurityToken GenerateJWT(IConfiguration config)
    {
        //creating claims to store in token
        Claim[] claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //creating security key
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Jwt-Key").Value));

        //creating signed credentials
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //generating token
        JwtSecurityToken token = new JwtSecurityToken(config["Issuer"],
          config["Issuer"],
          claims,
          expires: DateTime.Now.AddMonths(1),
          signingCredentials: creds);

        return token;


    }
}
