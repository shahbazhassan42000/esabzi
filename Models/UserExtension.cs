using bcrypt=BCrypt.Net.BCrypt;
using System.Net;
using Microsoft.IdentityModel.Tokens;

namespace esabzi.Models
{
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
            return !Name.IsNullOrEmpty() && !Email.IsNullOrEmpty() && !ContactNo.IsNullOrEmpty() && !Username.IsNullOrEmpty() && !Password.IsNullOrEmpty() && !Address.IsNullOrEmpty();
        }

        //validate login credentials
        public bool ValidateLogin()
        {
            return !Username.IsNullOrEmpty() && !Password.IsNullOrEmpty();
        }
    }
}
