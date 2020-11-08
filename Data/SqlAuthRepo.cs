     using CSP.Models;
using CSP.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSP.Data
{
    public class SqlAuthRepo: IAuthRepo
    {
   
        protected readonly IUserRepo _userService;
        protected readonly IConfiguration _config;

        public SqlAuthRepo(IUserRepo userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        public async Task<IEnumerable<User>> AuthenticateAsync(AuthenticateUser authUserVM)
        {
            byte[] bufferPassword = Encoding.ASCII.GetBytes(authUserVM.Password);
            byte[] hashedPassword = HashAlgorithm.Create("MD5").ComputeHash(bufferPassword);
            string encryptedPassword = Convert.ToBase64String(hashedPassword);

            authUserVM.Password = null;
            authUserVM.Password = encryptedPassword;

            var authUser = await _userService.FindByAsync(authUserVM);

            return authUser;
        }

        public AuthenticatedUserResult GetToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            Console.Write($"KEY >>>> {key}");
            Console.Write($"CREDS >>>> {creds}");

            var token = new JwtSecurityToken
                (
                    _config["Jwt:Site"],
                    _config["Jwt:Audience"],
                    null,
                    null,
                    DateTime.Now.AddMinutes(double.Parse(_config["Jwt:ExpiryInMinutes"])),
                    creds
                );

            Console.WriteLine(token.Issuer);

            return new AuthenticatedUserResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                FullName = user.Fullname
            };
        }
    }
}
