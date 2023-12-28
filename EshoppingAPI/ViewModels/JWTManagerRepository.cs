using EcommerceWebApi.Interfaces;

using EshoppingAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EshoppingAPI.ViewModels
{
    public class JWTManagerRepository : IJWTMangerRepository
    {
        Dictionary<string, string> UserRecords;

        private readonly IConfiguration configuration;
        private readonly EshoppingDBContext db;

        public JWTManagerRepository(IConfiguration _configuration, EshoppingDBContext _db)
        {
            db = _db;
            configuration = _configuration;
        }
        public Tokens Authenicate(LoginViewModel registerViewModel, bool IsRegister)
        {
            var _isAdmin = false;
            if (IsRegister)
            {
                if (db.Logins.Any(x => x.EmailId == registerViewModel.EmailId))
                {
                    return null;
                }
                Login tblLogin = new Login();
                tblLogin.EmailId = registerViewModel.EmailId;
                tblLogin.Password = registerViewModel.Password;
                tblLogin.FullName = registerViewModel.FullName;
                tblLogin.Address = registerViewModel.Address;
                tblLogin.Gender = registerViewModel.Gender;
                tblLogin.PhoneNumber = registerViewModel.PhoneNumber;
                db.Logins.Add(tblLogin);
                db.SaveChanges();
            }
            else
            {
                _isAdmin = db.Logins.Any(x => x.EmailId == registerViewModel.EmailId && x.Password == registerViewModel.Password && x.IsAdmin == 1);
            }
            UserRecords = db.Logins.ToList().ToDictionary(x => x.EmailId, x => x.Password);
            if (!UserRecords.Any(x => x.Key == registerViewModel.EmailId && x.Value == registerViewModel.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name,registerViewModel.EmailId)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token), IsAdmin = _isAdmin };
        }
    }
}