using System;
using System.Linq;
using System.Threading.Tasks;
using CSP.Data;
using CSP.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CSP.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController
    {
          private readonly IAuthRepo _authService;

        public AuthController(IAuthRepo authService)
        {
            _authService = authService;
        }
        [HttpPost("csp/Authenticate")]
        public async Task<AuthenticatedUserResult> Authenticate([FromBody] AuthenticateUser authUserVM)
        {
            try
            {
                var authUser = await this._authService.AuthenticateAsync(authUserVM);

                if (authUser == null)
                {
                    throw new Exception("User is unauthorized or credentials don't match");
                }

                return this._authService.GetToken(authUser.FirstOrDefault());
            }
            catch (Exception e) { throw e; }
        }
    }
}