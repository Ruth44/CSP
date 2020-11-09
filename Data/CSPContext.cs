using CSP.Models;
using Microsoft.EntityFrameworkCore;

namespace CSP.Data
{
    public class CSPContext : DbContext
    {
        public CSPContext(DbContextOptions<CSPContext> opt): base(opt){

        }
        public DbSet<Organization> Organizations{get; set; }
        public DbSet<Service> Services{get;set;}
        
         public DbSet<Ticket> Tickets{get;set;}
                  public DbSet<Request> Requests{get;set;}
                  public DbSet<User> Users{get;set;}

    }
}