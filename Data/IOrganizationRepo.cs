using System.Collections.Generic;
using CSP.Models;

namespace CSP.Data
{
    public interface IOrganizationRepo
    {
        bool SaveChanges();
        IEnumerable<Organization> GetAllOrganizations();
        Organization GetOrganizationById(int id);
        Organization GetOrganizationByName(string name);

        void CreateOrg(Organization org);

        void UpdateOrganization(Organization org);
        void DeleteOrganization(Organization org);
    }
}