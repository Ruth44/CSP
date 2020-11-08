using CSP.Models;
using CSP.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSP.Data
{
    public class SqlUserRepo: IUserRepo
    {

        protected readonly CSPContext _context;
        protected readonly DbSet<User> _userDbSet;

        public SqlUserRepo(CSPContext context)
        {
            _context = context;
            _userDbSet = _context.Set<User>();
        }
        public User GetUserById(int id){ 
            return _context.Users.FirstOrDefault(p=> p.Id==id);
        }
         public User GetUserByName(string name){ 
            return _context.Users.FirstOrDefault(p=> p.Username==name);
        }
        public async Task<User> AddAsync(User user)
        {
            try
            {
                byte[] bufferPassword = Encoding.ASCII.GetBytes(user.Password);
                byte[] hashedPassword = HashAlgorithm.Create("MD5").ComputeHash(bufferPassword);
                string encryptedPassword = Convert.ToBase64String(hashedPassword);

                user.Password = null;
                user.Password = encryptedPassword;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception e) { throw e; }
        }

        public async Task<IEnumerable<User>> FindByAsync(AuthenticateUser authUserVM)
        {
            try
            {
                var userName = authUserVM.Username;
                var password = authUserVM.Password;

                var user = await _context.Users.Where(u => u.Username == userName && u.Password == password).ToListAsync();

                return user;
            }
            catch (Exception e) { throw e; }
           
        }

        public async Task<IEnumerable<User>> GetAllAsync(Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null)
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                return users;
            }
            catch (Exception e) { throw e; }
        }

        public async Task<User> GetAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);

                return user;
            }
            catch (Exception e) { throw e; }
        }
    }
}
