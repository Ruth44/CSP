using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CSP.Models;

namespace CSP.Data
{
    public interface IServiceRepo
    {
        bool SaveChanges();
        void CreateService(Service ser);
        IEnumerable<Service> FindBy(Expression<Func<Service,bool>> ser);

        IEnumerable<Service> GetAllServices();
        Service GetServiceByOrganization(int id);

        Service GetServiceById(int id);
        Service GetServiceByName(string name);

        void UpdateService(Service ser);
        void DeleteService(Service ser);
    }
}