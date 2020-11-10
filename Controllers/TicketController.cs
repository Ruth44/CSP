using System.Collections.Generic;
using AutoMapper;
using CSP.Data;
using CSP.ViewModels;
using CSP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;

namespace CSP.Controllers{
    //api/songs
    [Route("api/Ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
private readonly IOrganizationRepo _repository;
private readonly IServiceRepo _repository2;
private readonly IRequestRepo _request;
private readonly ITicketRepo _ticket;
private readonly IUserRepo _userService;
        private readonly IAuthRepo _authService;



        private readonly IMapper _mapper;

        public TicketController(IOrganizationRepo repository ,ITicketRepo ticket, IServiceRepo repository2 , IMapper mapper, IAuthRepo authService, IUserRepo userService, IRequestRepo request)// IArtistRepo repository3)
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



   
        
     


 /// <summary>
        /// Get a Ticket by TicketNumber
        /// </summary>
        /// <param name="Ticket_number"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/csp/ticket/byticket/{Ticket_number}")]
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

     //////////////////////////////////////////////////////////////////////////
      /// <summary>
        /// Get Ticket with Service name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/csp/ticket/byservicename/{name}")]
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
      [HttpGet("/csp/ticket/byusername/{name}")]
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
     
 



        /// <summary>
        /// Get all tickets
        /// </summary>
        /// <returns></returns>
        [HttpGet("csp/tickets/")]
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
    
     







 /// Create a Ticket by taking in the userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
    //   POST api/songs
    [HttpPost("/csp/ticket/{userId}")]
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






/// <summary>
        /// Update a ticket using its Ticket Number
        /// </summary>
        /// <param name="ticUpdate"></param>
        /// <returns></returns>
    //PUT api/CSP/{id}
    [HttpPut("/csp/ticket/byticket/{ticket_number}")]
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
        /// Update part of a ticket
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
       //PATCH api/CSP/{id}
    
    [HttpPatch("/csp/ticket/bynumber/{number}")]
    public ActionResult PartialOrganizationUpdate(int id, JsonPatchDocument<CreateTicket> patchDoc)
{
      var orgModelFromRepo = _ticket.GetTicketByNumber(id);
        if(orgModelFromRepo == null){
            return NotFound();
        }
        var orgToPatch = _mapper.Map<CreateTicket>(orgModelFromRepo);
        patchDoc.ApplyTo(orgToPatch, ModelState);
        if(!TryValidateModel(orgToPatch))
        {
            return ValidationProblem(ModelState);
        }
      _mapper.Map(orgToPatch, orgModelFromRepo);
      _ticket.UpdateTicket(orgModelFromRepo);
      _ticket.SaveChanges();
      return NoContent();

}

    






/// <summary>
        /// Delete a Ticket using its Ticket Number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
// DELETE api/Organization/{id}
[HttpDelete("/csp/ticket/byticket/{ticket_number}")]
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