        using CSP.Models;
using CSP.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CSP.Data
{
    public interface IAuthRepo
    {

        Task<IEnumerable<User>> AuthenticateAsync(AuthenticateUser authUserVM);
        AuthenticatedUserResult GetToken(User user);
    }
}

