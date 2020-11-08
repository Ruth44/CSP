using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CSP.Models;

namespace CSP.Data
{
    public interface IRequestRepo
    {
        bool SaveChanges();
        IEnumerable<Request> GetAllRequests();
                Request FindBy(Expression<Func<Request,bool>> request);
                                IEnumerable<Request> FindByMany(Expression<Func<Request,bool>> ser);


         void CreateRequest(Request request);
        Request GetRequestById(int id);


        void UpdateRequest(Request request);
        void DeleteRequest(Request request);
    }
}