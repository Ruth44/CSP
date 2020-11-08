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
using Microsoft.AspNetCore.Authorization;

namespace CSP.Controllers{
    //api/songs
    [Route("api/CSP")]
    [ApiController]
    public class CSPController : ControllerBase
    {
private readonly IOrganizationRepo _repository;
private readonly IServiceRepo _repository2;
private readonly IRequestRepo _request;
private readonly ITicketRepo _ticket;
private readonly IUserRepo _userService;
        private readonly IAuthRepo _authService;

// private readonly IArtistRepo _repository3;


        private readonly IMapper _mapper;

        public CSPController(IOrganizationRepo repository ,ITicketRepo ticket, IServiceRepo repository2 , IMapper mapper, IAuthRepo authService, IUserRepo userService, IRequestRepo request)// IArtistRepo repository3)
       {
           _ticket=ticket;
           _request=request;
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
                Fullname = userVM.Fullname,
                Email=userVM.Email,
                Gender=userVM.Gender,
                Phone=userVM.Phone
            };


            return await this._userService.AddAsync(user);
        }

    //  GET api/songs
     /// <summary>
        /// Get all organizations
        /// </summary>
        /// <returns></returns>
        // [Authorize]
        [HttpGet("/Organization")]
      public ActionResult <IEnumerable<ReadOrganizations>> GetAllOrganizations()
      {
          var orgItems = _repository.GetAllOrganizations();
          return Ok(_mapper.Map<IEnumerable<ReadOrganizations>>(orgItems));
      }
     
      /// <summary>
        /// Get a Request by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/Requests/{id}")]
      public ActionResult <GetRequest> GetRequestById(int id)
      {
          var reqItem = _request.GetRequestById(id);
          if(reqItem==null){
               return NotFound("The Request could not be found.");
          }
          var getReq=_mapper.Map<GetRequest>(reqItem);
          getReq.ServiceName=_repository2.GetServiceById(reqItem.ServiceId).Name;
          getReq.Username=_userService.GetUserById(reqItem.UserId).Username;
      
          return Ok(getReq);
          
      }


 /// <summary>
        /// Get a Ticket by TicketNumber
        /// </summary>
        /// <param name="Ticket_number"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/Ticket/{Ticket_number}")]
      public ActionResult <GetTicket> GetTicketByNumber(int Ticket_number)
      {
          var reqItem = _ticket.GetTicketByNumber(Ticket_number);
          if(reqItem==null){
               return NotFound("The Ticket could not be found.");
          }
          var getReq=_mapper.Map<GetTicket>(reqItem);
          getReq.ServiceName=_repository2.GetServiceById(reqItem.ServiceId).Name;
          getReq.Username=_userService.GetUserById(reqItem.UserId).Username;
            getReq.FullName=_userService.GetUserById(reqItem.UserId).Fullname;

      
          return Ok(getReq);
          
      }





 /// <summary>
        /// Get Service with name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/Service/{name}")]
      public ActionResult <GetServices> GetServiceByName(string name)
      {
          var reqItem = _repository2.GetServiceByName(name);
          if(reqItem==null){
               return NotFound("The Service could not be found.");
          }
          var getReq=_mapper.Map<GetServices>(reqItem);
          getReq.OrganizationName=_repository.GetOrganizationById(reqItem.OrganizationId).Name;
         

      
          return Ok(getReq);
          
      }



 /// <summary>
        /// Get Service with Organization Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/Service/OrganizationName/{name}")]
      public ActionResult <CreateServices> GetServiceByOrgName(string name)
      {
          var orgId=_repository.GetOrganizationByName(name).Id;
          var serItem = _repository2.FindBy(t => t.OrganizationId == orgId);
          if(serItem != null){
              
            //   getReq.OrganizationName=name;
          return Ok(_mapper.Map<IEnumerable<CreateServices>>(serItem));

          }else return NotFound("The Service could not be found.");
      
          
      }

     //////////////////////////////////////////////////////////////////////////
      /// <summary>
        /// Get Ticket with Service name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/Ticket/ServiceName/{name}")]
      public ActionResult <CreateServices> GetTicketsBySerName(string name)
      {
          var serId=_repository2.GetServiceByName(name).Id;
          var ticItems = _ticket.FindByMany(t => t.ServiceId == serId);
          if(ticItems != null){
             var tic= _mapper.Map<IEnumerable<GetTicket>>(ticItems);
                      var requestss = ticItems.Zip(tic, (i, r) => new { Item = i, Request = r });
foreach(var req in requestss)
{
                     req.Request.ServiceName=_repository2.GetServiceById(req.Item.ServiceId).Name ;
                    req.Request.Username=_userService.GetUserById(req.Item.UserId).Username ;
                    req.Request.FullName=_userService.GetUserById(req.Item.UserId).Fullname;

}
          return Ok(tic);

          }else return NotFound("The Ticket could not be found.");
      } 
  //////////////////////////////////////////////////////////////////////////
      /// <summary>
        /// Get Ticket with userName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/Ticket/UserName/{name}")]
      public ActionResult <CreateServices> GetTicketsByUserName(string name)
      {
                   var uId=_userService.GetUserByName(name).Id;
          var ticItems = _ticket.FindByMany(t => t.UserId == uId);
          if(ticItems != null){
             var tic= _mapper.Map<IEnumerable<GetTicket>>(ticItems);
                      var requestss = ticItems.Zip(tic, (i, r) => new { Item = i, Request = r });
foreach(var req in requestss)
{
                     req.Request.ServiceName=_repository2.GetServiceById(req.Item.ServiceId).Name ;
                    req.Request.Username=_userService.GetUserById(req.Item.UserId).Username ;
                    req.Request.FullName=_userService.GetUserById(req.Item.UserId).Fullname;

}
          return Ok(tic);

          }else return NotFound("The Ticket could not be found.");
      
          
      } 
  //////////////////////////////////////////////////////////////////////////
      /// <summary>
        /// Get Request with Service Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/Request/ServiceName/{name}")]
      public ActionResult <GetRequest> GetRequestbySername(string name)
      {
           var serId=_repository2.GetServiceByName(name).Id;
          var ticItems = _request.FindByMany(t => t.ServiceId == serId);
          if(ticItems != null){
             var tic= _mapper.Map<IEnumerable<GetRequest>>(ticItems);
                      var requestss = ticItems.Zip(tic, (i, r) => new { Item = i, Request = r });
foreach(var req in requestss)
{
                      req.Request.ServiceName=_repository2.GetServiceById(req.Item.ServiceId).Name ;
                    req.Request.Username=_userService.GetUserById(req.Item.UserId).Username ;

}
          return Ok(tic);

          }else return NotFound("The Ticket could not be found.");
          
      } 
  //////////////////////////////////////////////////////////////////////////
      /// <summary>
        /// Get request with userName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/Request/UserName/{name}")]
       public ActionResult <GetRequest> GetRequestbyUsername(string name)
      {
           var serId=_userService.GetUserByName(name).Id;
          var ticItems = _request.FindByMany(t => t.UserId == serId);
          if(ticItems != null){
             var tic= _mapper.Map<IEnumerable<GetRequest>>(ticItems);
                      var requestss = ticItems.Zip(tic, (i, r) => new { Item = i, Request = r });
foreach(var req in requestss)
{
                      req.Request.ServiceName=_repository2.GetServiceById(req.Item.ServiceId).Name ;
                    req.Request.Username=_userService.GetUserById(req.Item.UserId).Username ;

}
          return Ok(tic);

          }else return NotFound("The Ticket could not be found.");
          
      
      } 
 
 /// <summary>
        /// Get Service with ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/Service/Byname/{id}")]
      public ActionResult <GetServices> GetServiceById(int id)
      {
          var reqItem = _repository2.GetServiceById(id);
          if(reqItem==null){
               return NotFound("The Service could not be found.");
          }
          var getReq=_mapper.Map<GetServices>(reqItem);
          getReq.OrganizationName=_repository.GetOrganizationById(reqItem.OrganizationId).Name;
         

      
          return Ok(getReq);
          
      }





          /// <summary>
        /// Get all requests
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Service")]
      public ActionResult <IEnumerable<GetServices>> GetAllServices()
      {
          var reqItems = _repository2.GetAllServices();
           var requests= _mapper.Map<IEnumerable<GetServices>>(reqItems);


        var requestss = reqItems.Zip(requests, (i, r) => new { Item = i, Request = r });
foreach(var req in requestss)
{
                       req.Request.OrganizationName=_repository.GetOrganizationById(req.Item.OrganizationId).Name ;
                    
}
return Ok(requests);
      }

           /// <summary>
        /// Get all requests
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Request")]
      public ActionResult <IEnumerable<GetRequest>> GetAllRequests()
      {
          var reqItems = _request.GetAllRequests();
           var requests= _mapper.Map<IEnumerable<GetRequest>>(reqItems);


        var requestss = reqItems.Zip(requests, (i, r) => new { Item = i, Request = r });
foreach(var req in requestss)
{
                       req.Request.ServiceName=_repository2.GetServiceById(req.Item.ServiceId).Name ;
                    req.Request.Username=_userService.GetUserById(req.Item.UserId).Username ;

}
          
               
          return Ok(requests);
      }
        /// <summary>
        /// Get all tickets
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Tickets")]
      public ActionResult <IEnumerable<GetTicket>> GetAllTickets()
      {
          var reqItems = _ticket.GetAllTickets();
           var requests= _mapper.Map<IEnumerable<GetTicket>>(reqItems);


        var requestss = reqItems.Zip(requests, (i, r) => new { Item = i, Request = r });
foreach(var req in requestss)
{
                       req.Request.ServiceName=_repository2.GetServiceById(req.Item.ServiceId).Name ;
                    req.Request.Username=_userService.GetUserById(req.Item.UserId).Username ;
                    req.Request.FullName=_userService.GetUserById(req.Item.UserId).Fullname;

}
          
               
          return Ok(requests);
      }
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
        return Ok("Successfully inserted "+ org.Name);
    }
/// <summary>
        /// Create a service by taking in the organization name
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
    //   POST api/songs
    [HttpPost("/Service/{name}")]
    public ActionResult <CreateServices> CreateNewService(string name,CreateServices ser){
int id=_repository.GetOrganizationByName(name).Id;
         var serModel = _mapper.Map<Service>(ser);
         serModel.OrganizationId=id;
        _repository2.CreateService(serModel);
        _repository2.SaveChanges();

        return Ok("Successfully inserted "+ ser.Name);
    }


/// <summary>
        /// Create a Request by taking in the userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
    //   POST api/songs
    [HttpPost("/Request/{userId}")]
    public ActionResult <CreateRequest> CreateNewRequest(int userId,CreateRequest req){
int id=_repository2.GetServiceByName(req.ServiceName).Id;
         var reqModel = _mapper.Map<Request>(req);
         reqModel.UserId=userId;
         reqModel.ServiceId=id;
         reqModel.Status="Ongoing";
         reqModel.CreatedAt=DateTime.Now;
        _request.CreateRequest(reqModel);
        _request.SaveChanges();

        return Ok("Successfully created on!");
    }

 /// Create a Ticket by taking in the userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
    //   POST api/songs
    [HttpPost("/Ticket/{userId}")]
    public ActionResult <CreateTicket> CreateNewTicket(int userId,CreateTicket tic){

         var ticModel = _mapper.Map<Ticket>(tic);
         ticModel.UserId=userId;
         ticModel.ServiceId=_repository2.GetServiceByName(tic.ServiceName).Id;
         ticModel.CreatedAt=DateTime.Now;
         ticModel.TicketNumber=new Random().Next(100000,999999999);
        _ticket.CreateTicket(ticModel);
        _request.SaveChanges();

        return Ok("Successfully created on!");
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
        /// Update a request using its ID
        /// </summary>
        /// <param name="orgUpdate"></param>
        /// <returns></returns>
    //PUT api/CSP/{id}
    [HttpPut("/Request/{id}")]
    public ActionResult UpdateRequestById(int id, CreateRequest reqUpdate)
    {
        var reqModelFromRepo = _request.GetRequestById(id);
        if(reqModelFromRepo == null){
            return NotFound();
        }
        _mapper.Map(reqUpdate, reqModelFromRepo);
        reqModelFromRepo.ServiceId= _repository2.GetServiceByName(reqUpdate.ServiceName).Id;
        _request.UpdateRequest(reqModelFromRepo);
        _request.SaveChanges();
        return NoContent();
    }

/// <summary>
        /// Update a ticket using its Ticket Number
        /// </summary>
        /// <param name="ticUpdate"></param>
        /// <returns></returns>
    //PUT api/CSP/{id}
    [HttpPut("/Ticket/{ticket_number}")]
    public ActionResult UpdateTicketById(int ticket_number, CreateTicket ticUpdate)
    {
        var ticModelFromRepo = _ticket.GetTicketByNumber(ticket_number);
        if(ticModelFromRepo == null){
            return NotFound("No such Ticket");
        }
        _mapper.Map(ticUpdate, ticModelFromRepo);
        ticModelFromRepo.ServiceId= _repository2.GetServiceByName(ticUpdate.ServiceName).Id;
        _ticket.UpdateTicket(ticModelFromRepo);
        _ticket.SaveChanges();
        return Ok("Updated Successfuly");
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
        /// Update a service using its ID
        /// </summary>
        /// <param name="serUpdate"></param>
        /// <returns></returns>
    //PUT api/CSP/{id}
    [HttpPut("/Service/Update/{id}")]
    public ActionResult UpdateServiceById(int id, CreateServices serUpdate)
    {
        var serModelFromRepo = _repository2.GetServiceById(id);
        if(serModelFromRepo == null){
            return NotFound();
        }
        _mapper.Map(serUpdate, serModelFromRepo);
        _repository2.UpdateService(serModelFromRepo);
        _repository2.SaveChanges();
        return NoContent();
    }

  /// <summary>
        /// Update a service using its Name
        /// </summary>
        /// <param name="serUpdate"></param>
        /// <returns></returns>
    //PUT api/CSP/hh/{}
    [HttpPut("/Service/{service_name}")]
    public ActionResult UpdateServiceByName(string service_name, CreateServices serUpdate)
    {
        var serModelFromRepo = _repository2.GetServiceByName(service_name);
        if(serModelFromRepo == null){
            return NotFound();
        }
        _mapper.Map(serUpdate, serModelFromRepo);
        _repository2.UpdateService(serModelFromRepo);
        _repository2.SaveChanges();
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


/// <summary>
        /// Delete a service using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
// DELETE api/Organization/{id}
[HttpDelete("/Service/{id}")]
public ActionResult DeleteServicebyId(int id)
{
    var toBeDeleted= _repository2.GetServiceById(id);
  if(toBeDeleted==null){
      return NotFound("Service not found!");
  }
        _repository2.DeleteService(toBeDeleted);
    _repository2.SaveChanges();
      
    return Ok("Deleted Successfully");
}

/// <summary>
        /// Delete a request using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
// DELETE api/Organization/{id}
[HttpDelete("/Request/{id}")]
public ActionResult DeleteRequestbyId(int id)
{
    var toBeDeleted= _request.GetRequestById(id);
  if(toBeDeleted==null){
      return NotFound("Request not found!");
  }
        _request.DeleteRequest(toBeDeleted);
    _request.SaveChanges();
      
    return Ok("Deleted Successfully");
}


/// <summary>
        /// Delete a Ticket using its Ticket Number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
// DELETE api/Organization/{id}
[HttpDelete("/Ticket/{ticket_number}")]
public ActionResult DeleteTicketbyNumber(int ticket_number)
{
    var toBeDeleted= _ticket.GetTicketByNumber(ticket_number);
  if(toBeDeleted==null){
      return NotFound("Service not found!");
  }
        _ticket.DeleteTicket(toBeDeleted);
    _ticket.SaveChanges();
      
    return Ok("Deleted Successfully");
}




    }
}