using CSP.Models;
using Microsoft.EntityFrameworkCore;

namespace CSP.Data
{
    public class CSPContext : DbContext
    {
        public CSPContext(DbContextOptions<CSPContext> opt): base(opt){

        }
        public virtual DbSet<Organization> Organizations{get; set; }
        public virtual DbSet<Service> Services{get;set;}
        
         public virtual DbSet<Ticket> Tickets{get;set;}
                  public virtual DbSet<Request> Requests{get;set;}
                  public virtual DbSet<User> Users{get;set;}

    }
}