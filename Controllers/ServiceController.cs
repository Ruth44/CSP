using System.Collections.Generic;
using AutoMapper;
using CSP.Data;
using CSP.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CSP.Models;
using System;
using System.Linq;

namespace CSP.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController: ControllerBase
    {
        private readonly IOrganizationRepo _repository;
private readonly IServiceRepo _repository2;
private readonly IRequestRepo _request;
private readonly ITicketRepo _ticket;
private readonly IUserRepo _userService;
        private readonly IAuthRepo _authService;

// private readonly IArtistRepo _repository3;


        private readonly IMapper _mapper;

        public ServiceController(IOrganizationRepo repository ,ITicketRepo ticket, IServiceRepo repository2 , IMapper mapper, IAuthRepo authService, IUserRepo userService, IRequestRepo request)// IArtistRepo repository3)
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
        /// Get Service with name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/ByName/{name}")]
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
      [HttpGet("/ByOrganizationName/{name}")]
      public ActionResult <CreateServices> GetServiceByOrgName(string name)
      {
          var orgId=_repository.GetOrganizationByName(name).Id;
          var serItem = _repository2.FindBy(t => t.OrganizationId == orgId);
          if(serItem != null){
              
            //   getReq.OrganizationName=name;
          return Ok(_mapper.Map<IEnumerable<CreateServices>>(serItem));

          }else return NotFound("The Service could not be found.");
      
          
      }

     
 
 /// <summary>
        /// Get Service with ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("/ById/{id}")]
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
        [HttpGet]
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
        /// Create a service by taking in the organization name
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
    //   POST api/songs
    [HttpPost("/{name}")]
    public ActionResult <CreateServices> CreateNewService(string name,CreateServices ser){
int id=_repository.GetOrganizationByName(name).Id;
         var serModel = _mapper.Map<Service>(ser);
         serModel.OrganizationId=id;
        _repository2.CreateService(serModel);
        _repository2.SaveChanges();

        return Ok("Successfully inserted "+ ser.Name);
    }











 /// <summary>
        /// Update a service using its ID
        /// </summary>
        /// <param name="serUpdate"></param>
        /// <returns></returns>
    //PUT api/CSP/{id}
    [HttpPut("/ByID/{id}")]
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
    [HttpPut("/ByServiceName/{service_name}")]
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
        /// Delete a service using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
// DELETE api/Organization/{id}
[HttpDelete("/ById/{id}")]
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


    }
}