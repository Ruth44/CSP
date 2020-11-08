using System.Threading.Tasks;
using CSP.Data;
using CSP.Models;
using CSP.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace CSP.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController
    {
         private readonly IUserRepo _userService;

        public UserController(IUserRepo userService)
        {
            _userService = userService;
        }
        /// Create Account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateAccount")]
        public async Task<User> CreateAccount([FromBody] CreateUserAccount userVM)
        {
            User user = new User()
            {
                Username = userVM.Username,
                Password = userVM.Password,
                Fullname = userVM.Fullname,
                Email=userVM.Email,
                Gender=userVM.Gender,
                Phone=userVM.Phone
            };


            return await this._userService.AddAsync(user);
        }

    }
}