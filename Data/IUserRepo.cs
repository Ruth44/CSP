        using CSP.Models;
using CSP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSP.Data
{
    public interface IUserRepo
    {

        Task<IEnumerable<User>> GetAllAsync(Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null);
        Task<User> GetAsync(int id);
        Task<User> AddAsync(User user);
        Task<IEnumerable<User>> FindByAsync(AuthenticateUser authUserVM);
    }
}
