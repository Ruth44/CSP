using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CSP.Models;

namespace CSP.Data{
    public class SqlRequestRepo : IRequestRepo
    {
        private readonly CSPContext _context;
                private readonly DbSet<Request> _request;



        public SqlRequestRepo(CSPContext context)
        {
            _context=context;
            _request=_context.Set<Request>();
                    
                    

        }

        public bool SaveChanges()
        {
          return (_context.SaveChanges() >= 0);
        }

  

        void IRequestRepo.DeleteRequest(Request request)
        {
if(request==null){
            throw new ArgumentNullException(nameof(request));

          }
          _context.Requests.Remove(request);        }

       

        void IRequestRepo.UpdateRequest(Request request)
{
        }
public IEnumerable<Request> FindByMany(Expression<Func<Request,bool>> ser)
        {
  return _request.Where(ser).ToList();
                        // return test;       
                         }
 
        IEnumerable<Request> IRequestRepo.GetAllRequests()
        {
            
                
  return _context.Requests.ToList();       
        }

        void IRequestRepo.CreateRequest(Request request)
        {
if(request==null){
            throw new ArgumentNullException(nameof(request));
          }
          _context.Requests.Add(request);                }

        Request IRequestRepo.GetRequestById(int id)
        {
return _context.Requests.FirstOrDefault(p=> p.Id==id);
        }

        Request IRequestRepo.FindBy(Expression<Func<Request, bool>> cmd)
        {
 var test = _request.Where(cmd).FirstOrDefault();
                        return test;        }
    }
}