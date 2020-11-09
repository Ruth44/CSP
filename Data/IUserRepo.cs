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
        User GetUserById(int id);
        User GetUserByName(string name);
        User GetUserByEmail(string name);
        User GetUserByPhone(string name);
                void UpdateUser(User org);
        bool SaveChanges();

        Task<IEnumerable<User>> FindByAsync(AuthenticateUser authUserVM);
    }
}
