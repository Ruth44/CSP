using System;
using System.Collections.Generic;
using System.Linq;
using CSP.Models;

namespace CSP.Data{
    public class SqlOrganizationRepo : IOrganizationRepo
    {
        private readonly CSPContext _context;

        public SqlOrganizationRepo(CSPContext context)
        {
            _context=context;
        }

        public void CreateOrg(Organization org)
        {
          if(org==null){
            throw new ArgumentNullException(nameof(org));
          }
          _context.Organizations.Add(org);
            }

        public void DeleteOrganization(Organization org)
        {
          if(org==null){
            throw new ArgumentNullException(nameof(org));

          }
          _context.Organizations.Remove(org);
        }

        public IEnumerable<Organization> GetAllOrganizations()
        {
          return _context.Organizations.ToList();
          }

        public Organization GetOrganizationById(int id)
        {
return _context.Organizations.FirstOrDefault(p=> p.Id==id);
        }
         public Organization GetOrganizationByName(string name)
        {
return _context.Organizations.FirstOrDefault(p=> p.Name==name);
        }

        public bool SaveChanges()
        {
          return (_context.SaveChanges() >= 0);
        }

        public void UpdateOrganization(Organization org)
        {
            
        }
    }
}