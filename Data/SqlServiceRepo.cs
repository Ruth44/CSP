using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CSP.Models;

namespace CSP.Data{
    public class SqlServiceRepo : IServiceRepo
    {
        private readonly CSPContext _context;
        private readonly DbSet<Service> _service;


        public SqlServiceRepo(CSPContext context)
        {
            _context=context;
            _service=_context.Set<Service>();

        }
      
        public bool SaveChanges()
        {
          return (_context.SaveChanges() >= 0);
        }


//         void IArtistRepo.CreateArtist(Artist song)
//         {
//  if(song==null){
//             throw new ArgumentNullException(nameof(song));
//           }
//           _context.Artists.Add(song);        }

        public void DeleteService(Service ser)
        {
if(ser==null){
            throw new ArgumentNullException(nameof(ser));

          }
          _context.Services.Remove(ser);      
            }

        public IEnumerable<Service> FindBy(Expression<Func<Service,bool>> ser)
        {
  return _service.Where(ser);
                        // return test;       
                         }

  //       IEnumerable<Artist> IArtistRepo.GetAllArtists()
  //       {
  // return _context.Artists.ToList();       
  //  }

        public Service GetServiceById(int id)
        {
return _context.Services.FirstOrDefault(p=> p.Id==id);
        }
         Service IServiceRepo.GetServiceByOrganization(int id){
           return _context.Services.FirstOrDefault(p=> p.Organization.Id==id);
        }


//         void IArtistRepo.UpdateArtist(Artist artist)
//         {

//         }
    }
}