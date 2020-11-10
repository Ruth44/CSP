using System.Collections.Generic;
using AutoMapper;
using CSP.Data;
using CSP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CSP.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace CSP.Controllers
{
    [Route("api/request")]
    [ApiController]
    public class RequestController: ControllerBase
    {
          private readonly IOrganizationRepo _repository;
private readonly IServiceRepo _repository2;
private readonly IRequestRepo _request;
private readonly ITicketRepo _ticket;
private readonly IUserRepo _userService;
        private readonly IAuthRepo _authService;
                private readonly IMapper _mapper;

        public RequestController(IOrganizationRepo repository ,ITicketRepo ticket, IServiceRepo repository2 , IMapper mapper, IAuthRepo authService, IUserRepo userService, IRequestRepo request)// IArtistRepo repository3)
       {
           _ticket=ticket;
           _request=request;
           _userService=userService;
           _authService=authService;
        _repository2= repository2;
           _repository = repository;
           _mapper = mapper;
       }

/// <summary>
        /// Create a Request by taking in the userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
    //   POST api/songs
    [HttpPost("csp/request/{userId}")]
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

           /// <summary>
        /// Get all requests
        /// </summary>
        /// <returns></returns>
        [HttpGet("csp/requests")]
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
       /// Update part of a Request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       //PATCH api/CSP/{id}
    
    [HttpPatch("/csp/request/byid/{id}")]
    public ActionResult PartialOrganizationUpdate(int id, JsonPatchDocument<CreateRequest> patchDoc)
{
      var orgModelFromRepo = _request.GetRequestById(id);
        if(orgModelFromRepo == null){
            return NotFound();
        }
        var orgToPatch = _mapper.Map<CreateRequest>(orgModelFromRepo);
        patchDoc.ApplyTo(orgToPatch, ModelState);
        if(!TryValidateModel(orgToPatch))
        {
            return ValidationProblem(ModelState);
        }
      _mapper.Map(orgToPatch, orgModelFromRepo);
      _request.UpdateRequest(orgModelFromRepo);
      _request.SaveChanges();
      return NoContent();

}
 /// <summary>
        /// Get Request with Service Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/csp/request/byname/{name}")]
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
       /// <summary>
        /// Get a Request by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/csp/request/byid/{id}")]
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
  //////////////////////////////////////////////////////////////////////////
      /// <summary>
        /// Get request with userName
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/csp/request/byusername/{name}")]
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
        /// Update a request using its ID
        /// </summary>
        /// <param name="reqUpdate"></param>
        /// <returns></returns>
    [HttpPut("/csp/request/byid/{id}")]
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
        /// Delete a request using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

// DELETE api/Organization/{id}
[HttpDelete("/csp/request/byid/{id}")]
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
    }
}