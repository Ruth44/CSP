using System.Collections.Generic;
using AutoMapper;
using CSP.Data;
using CSP.ViewModels;
using CSP.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace CSP.Controllers{
    //api/songs
    [Route("api/CSP")]
    [ApiController]
    public class CSPController : ControllerBase
    {
private readonly IOrganizationRepo _repository;
private readonly IServiceRepo _repository2;
private readonly IUserRepo _userService;
        private readonly IAuthRepo _authService;

// private readonly IArtistRepo _repository3;


        private readonly IMapper _mapper;

        public CSPController(IOrganizationRepo repository , IServiceRepo repository2 , IMapper mapper, IAuthRepo authService, IUserRepo userService)// IArtistRepo repository3)
       {
           _userService=userService;
           _authService=authService;
        _repository2= repository2;
           _repository = repository;
        //    _repository3=repository3;
           _mapper = mapper;
       }

[HttpPost("/Authenticate")]
        public async Task<AuthenticatedUserResult> Authenticate([FromBody] AuthenticateUser authUserVM)
        {
            try
            {
                var authUser = await this._authService.AuthenticateAsync(authUserVM);

                if (authUser == null)
                {
                    throw new Exception("User is unauthorized or credentials don't match");
                }

                return this._authService.GetToken(authUser.FirstOrDefault());
            }
            catch (Exception e) { throw e; }
        }
/// Create Account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("/User")]
        public async Task<User> CreateAccount([FromBody] CreateUserAccount userVM)
        {
            User user = new User()
            {
                Username = userVM.Username,
                Password = userVM.Password,
                Fullname = userVM.Fullname
            };


            return await this._userService.AddAsync(user);
        }

    //  GET api/songs
     /// <summary>
        /// Get all organizations
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Organization")]
      public ActionResult <IEnumerable<ReadOrganizations>> GetAllOrganizations()
      {
          var orgItems = _repository.GetAllOrganizations();
          return Ok(_mapper.Map<IEnumerable<ReadOrganizations>>(orgItems));
      }
    //   /// <summary>
    //     /// Get a song by using its ID
    //     /// </summary>
    //     /// <param name="id"></param>
    //     /// <returns></returns>
    // //   GET api/songs/$
    //   [HttpGet("{id}", Name="GetSongById")]
    //   public ActionResult <ReadSongs> GetSongById(int id)
    //   {
    //       var songItem = _repository.GetSongById(id);
    //       if(songItem != null){
    //       return Ok(_mapper.Map<ReadSongs>(songItem));

    //       }
    //       return NotFound();
    //   }
    //    /// Get an album by using its name(USING PREDICATE)
    //     /// </summary>
    //     /// <param name="name"></param>
    //     /// <returns></returns>
    // //   GET api/songs/$
    //   [HttpGet("/album/{name}", Name="GetAlbumByName")]
    //   public ActionResult <ReadAlbums> GetAlbumByName(string name)
    //   {
    //       var songItem = _repository2.FindBy(t => t.Name == name);
    //       if(songItem != null){
    //       return Ok(_mapper.Map<ReadAlbums>(songItem));

    //       }
    //       return NotFound();
    //   }

    //     /// Get services using their organization name
    //     /// </summary>
    //     /// <param name="name"></param>
    //     /// <returns></returns>
    // //   GET api/songs/$
    //   [HttpGet("/Services/{name}", Name="GetServiceByOrganizationName")]
    //   public ActionResult <ReadServices> GetServicesByOrganizationName(string name)
    //   {
    //        var orgId= _repository.GetOrganizationByName(name).Id;
    // var toBeViewed= _repository2.FindBy(t => t.OrganizationId == orgId);
    // IEnumerable<ReadServices> test2 = toBeViewed;
     
  
    //       if(toBeViewed != null){
    //       return Ok(test2);

    //       }
    //       return NotFound();
    //   }
     


/// <summary>
        /// Create an organization with services
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
    //   POST api/songs
    [HttpPost("/Organization")]
    public ActionResult <Organization> CreateOrganization(Organization org){
        // var songModel = _mapper.Map<Song>(songCreateDto);
      
     
        // Album test = new Album()
        // {
        //     Name=albumName,
        //     ReleaseYear=releaseYear
        // }; 
        // Artist test2 = new Artist()
        // {
        //     Name=artistName,
        //     Nationality=nationality
        // }; 
        _repository.CreateOrg(org);
        _repository.SaveChanges();
        //  var savedAlbum1 = _repository2.FindBy(t => t.Name == test.Name);
        // var savedAlbum2 = _repository2.FindBy(t => t.ReleaseYear == test.ReleaseYear);
        // _repository3.CreateArtist(test2);
        // _repository3.SaveChanges();

        // var savedArtist1 = _repository3.FindBy(t => t.Name == test2.Name);
        // var savedArtist2 = _repository3.FindBy(t => t.Nationality == test2.Nationality);

        // songModel.AlbumId = savedAlbum1.Id;
        // songModel.ArtistId=savedArtist1.Id;
        // _repository.CreateSong(songModel);
        // _repository.SaveChanges();

        // var songReadDto = _mapper.Map<MusicCreate>(songModel);
        return Ok("Successfully inserted.");
    }













//       /// <summary>
//         /// Create a song along with its album and artist
//         /// </summary>
//         /// <param name="songCreateDto"></param>
//         /// <returns></returns>
//     //   POST api/songs
//     [HttpPost]
//     public ActionResult <MusicCreate> CreateSong(MusicCreate songCreateDto){
//         var songModel = _mapper.Map<Song>(songCreateDto);
//         var albumName= songCreateDto.albumName;
//         var artistName= songCreateDto.artistName;
//         var releaseYear= songCreateDto.albumReleaseYear;
//         var nationality=songCreateDto.artistNationality;

//         Album test = new Album()
//         {
//             Name=albumName,
//             ReleaseYear=releaseYear
//         }; 
//         Artist test2 = new Artist()
//         {
//             Name=artistName,
//             Nationality=nationality
//         }; 
//         _repository2.CreateAlbum(test);
//         _repository2.SaveChanges();
//          var savedAlbum1 = _repository2.FindBy(t => t.Name == test.Name);
//         var savedAlbum2 = _repository2.FindBy(t => t.ReleaseYear == test.ReleaseYear);
//         _repository3.CreateArtist(test2);
//         _repository3.SaveChanges();

//         var savedArtist1 = _repository3.FindBy(t => t.Name == test2.Name);
//         var savedArtist2 = _repository3.FindBy(t => t.Nationality == test2.Nationality);

//         songModel.AlbumId = savedAlbum1.Id;
//         songModel.ArtistId=savedArtist1.Id;
//         _repository.CreateSong(songModel);
//         _repository.SaveChanges();

//         var songReadDto = _mapper.Map<MusicCreate>(songModel);
//         return Ok(songReadDto);
//     }
    /// <summary>
        /// Update an organization using their ID
        /// </summary>
        /// <param name="orgUpdate"></param>
        /// <returns></returns>
    //PUT api/CSP/{id}
    [HttpPut("/Organization/Update/{id}")]
    public ActionResult UpdateOrganizationById(int id, ReadOrganizations orgUpdate)
    {
        var orgModelFromRepo = _repository.GetOrganizationById(id);
        if(orgModelFromRepo == null){
            return NotFound();
        }
        _mapper.Map(orgUpdate, orgModelFromRepo);
        _repository.UpdateOrganization(orgModelFromRepo);
        _repository.SaveChanges();
        return NoContent();
    }

  /// <summary>
        /// Update an organization using their Name
        /// </summary>
        /// <param name="orgUpdate"></param>
        /// <returns></returns>
    //PUT api/CSP/hh/{}
    [HttpPut("/Organization/{organization_name}")]
    public ActionResult UpdateOrganizationByName(string organization_name, ReadOrganizations orgUpdate)
    {
        var orgModelFromRepo = _repository.GetOrganizationByName(organization_name);
        if(orgModelFromRepo == null){
            return NotFound();
        }
        _mapper.Map(orgUpdate, orgModelFromRepo);
        _repository.UpdateOrganization(orgModelFromRepo);
        _repository.SaveChanges();
        return NoContent();
    }

     /// <summary>
//         /// Update part of an organization
//         /// </summary>
//         /// <param name="id"></param>
//         /// <returns></returns>
//        //PATCH api/CSP/{id}
    
//     [HttpPatch("{id}")]
//     public ActionResult PartialOrganizationUpdate(int id, JsonPatchDocument<ReadOrganizations> patchDoc)
// {
//       var orgModelFromRepo = _repository.GetOrganizationById(id);
//         if(orgModelFromRepo == null){
//             return NotFound();
//         }
//         var orgToPatch = _mapper.Map<ReadOrganizations>(orgModelFromRepo);
//         patchDoc.ApplyTo(orgToPatch, ModelState);
//         if(!TryValidateModel(orgToPatch))
//         {
//             return ValidationProblem(ModelState);
//         }
//       _mapper.Map(orgToPatch, orgModelFromRepo);
//       _repository.UpdateOrganization(orgModelFromRepo);
//       _repository.SaveChanges();
//       return NoContent();

// }
 /// <summary>
        /// Delete an organization using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
// DELETE api/Organization/{id}
[HttpDelete("/Organization/{id}")]
public ActionResult DeleteOrganizationbyId(int id)
{
    var toBeDeleted= _repository2.GetServiceByOrganization(id);
    while (toBeDeleted!=null)
    {
        _repository2.DeleteService(toBeDeleted);
    _repository2.SaveChanges();
         toBeDeleted= _repository2.GetServiceById(id);

    }
    var orgModelFromRepo = _repository.GetOrganizationById(id);
    if(orgModelFromRepo==null){
        return NotFound();
    }
    _repository.DeleteOrganization(orgModelFromRepo);
    _repository.SaveChanges();
    return NoContent();
}
        /// Delete an organization using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
// DELETE api/Organization/{id}
[HttpDelete("/Organization/Delete/{name}")]
public ActionResult DeleteOrganizationbyName(string name)
{
    var orgId= _repository.GetOrganizationByName(name).Id;
    var toBeDeleted= _repository2.GetServiceByOrganization(orgId);

    while (toBeDeleted!=null)
    {
        _repository2.DeleteService(toBeDeleted); 
    _repository2.SaveChanges();
         toBeDeleted= _repository2.GetServiceByOrganization(orgId);

    }
    var orgModelFromRepo = _repository.GetOrganizationById(orgId);
    if(orgModelFromRepo==null){
        return NotFound();
    }
    _repository.DeleteOrganization(orgModelFromRepo);
    _repository.SaveChanges();
    return NoContent();
}
    }
}