// using System;
// using System.Collections.Generic;
// using System.Linq.Expressions;
// using Music.Models;

// namespace Music.Data
// {
//     public interface IAlbumRepo
//     {
//         bool SaveChanges();
//         IEnumerable<Album> GetAllAlbums();
//                 Album FindBy(Expression<Func<Album,bool>> cmd);

//          void CreateAlbum(Album album);
//         Album GetAlbumById(int id);


//         void UpdateAlbum(Album album);
//         void DeleteAlbum(Album album);
//     }
// }