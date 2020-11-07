// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Linq.Expressions;
// using Microsoft.EntityFrameworkCore;
// using Music.Models;

// namespace Music.Data{
//     public class SqlAlbumRepo : IAlbumRepo
//     {
//         private readonly MusicContext _context;
//                 private readonly DbSet<Album> _album;



//         public SqlAlbumRepo(MusicContext context)
//         {
//             _context=context;
//             _album=_context.Set<Album>();
                    
                    

//         }

//         public bool SaveChanges()
//         {
//           return (_context.SaveChanges() >= 0);
//         }

  

//         void IAlbumRepo.DeleteAlbum(Album album)
//         {
// if(album==null){
//             throw new ArgumentNullException(nameof(album));

//           }
//           _context.Albums.Remove(album);        }

       

//         void IAlbumRepo.UpdateAlbum(Album album)
// {
//         }

//         IEnumerable<Album> IAlbumRepo.GetAllAlbums()
//         {
//   return _context.Albums.ToList();       
//         }

//         void IAlbumRepo.CreateAlbum(Album album)
//         {
// if(album==null){
//             throw new ArgumentNullException(nameof(album));
//           }
//           _context.Albums.Add(album);                }

//         Album IAlbumRepo.GetAlbumById(int id)
//         {
// return _context.Albums.FirstOrDefault(p=> p.Id==id);
//         }

//         Album IAlbumRepo.FindBy(Expression<Func<Album, bool>> cmd)
//         {
//  var test = _album.Where(cmd).FirstOrDefault();
//                         return test;        }
//     }
// }