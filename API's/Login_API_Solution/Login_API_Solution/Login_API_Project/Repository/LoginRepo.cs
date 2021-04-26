using Login_API_Project.Models;
using Login_API_Project.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Login_API_Project.Repository
{
    public class LoginRepo : ILoginRepo
    {
        testContext db = new testContext();
        private testContext @object;
        private readonly string tokenKey;
        public LoginRepo(string tokenKey)
        {
            this.tokenKey = tokenKey;
        }

        public LoginRepo(testContext @object)
        {
            this.@object = @object;
        }

        public string AuthenticateUser(string username, string password)
        {
            if (!db.LoginDetails.Any(u => u.Username == username && u.Password == password))
            {
                
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //public LoginDetail login(string username, string password)
        //{
        //    LoginDetail log = db.LoginDetails.FirstOrDefault(u => u.UserName == username && u.Password == password );
        //    LoginDetail loginDetail = new LoginDetail();
        //    if (log != null)
        //    {
        //        loginDetail.Loginas = log.Loginas;
        //    }
        //    return loginDetail;
        //}
    }
}
