using System.Collections.Generic;
using AutoMapper;
using CSP.Data;
using CSP.ViewModels;
using Microsoft.AspNetCore.Mvc;

using CSP.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace CSP.Controllers
{
[Route("api/organiation")]
    [ApiController]
    public class OrganizationController: ControllerBase
    {
            private readonly IOrganizationRepo _repository;
private readonly IServiceRepo _repository2;
private readonly IRequestRepo _request;
private readonly ITicketRepo _ticket;
private readonly IUserRepo _userService;
        private readonly IAuthRepo _authService;
                private readonly IMapper _mapper;

        public OrganizationController(IOrganizationRepo repository ,ITicketRepo ticket, IServiceRepo repository2 , IMapper mapper, IAuthRepo authService, IUserRepo userService, IRequestRepo request)// IArtistRepo repository3)
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
        /// Get all organizations
        /// </summary>
        /// <returns></returns>
        // [Authorize]
        [HttpGet]
        [Route("csp/organizations")]
      public ActionResult <IEnumerable<ReadOrganizations>> GetAllOrganizations()
      {
          var orgItems = _repository.GetAllOrganizations();
          return Ok(_mapper.Map<IEnumerable<ReadOrganizations>>(orgItems));
      }
/// <summary>
        /// Create an organization with services
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
    //   POST api/songs
    [HttpPost]
    [Route("csp/organizations")]
    // [Authorize]
    public ActionResult <ReadOrganizations> CreateOrganization(ReadOrganizations org){
        var orgData=_repository.GetOrganizationByName(org.Name);
if(orgData==null){
            // _mapper.Map(org, orgData);
         var reqModel = _mapper.Map<Organization>(org);

  _repository.CreateOrg(reqModel);
        _repository.SaveChanges();

        return Ok("Successfully inserted "+ org.Name);
}else{
    return NotFound("Organization already exists");
}
      
    }
        /// <summary>
        /// Update an organization using their ID
        /// </summary>
        /// <param name="orgUpdate"></param>
        /// <returns></returns>
    //PUT api/CSP/{id}
    [HttpPut]
    [Route("csp/organization/byid/{id}")]
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
     /// Update part of an organization
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       //PATCH api/CSP/{id}
    
    [HttpPatch]
        [Route("csp/organization/byid/{id}")]

    public ActionResult PartialOrganizationUpdate(int id, JsonPatchDocument<ReadOrganizations> patchDoc)
{
      var orgModelFromRepo = _repository.GetOrganizationById(id);
        if(orgModelFromRepo == null){
            return NotFound();
        }
        var orgToPatch = _mapper.Map<ReadOrganizations>(orgModelFromRepo);
        patchDoc.ApplyTo(orgToPatch, ModelState);
        if(!TryValidateModel(orgToPatch))
        {
            return ValidationProblem(ModelState);
        }
      _mapper.Map(orgToPatch, orgModelFromRepo);
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
    [HttpPut("csp/organization/byname/{organization_name}")]
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
        /// Delete an organization using its ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
// DELETE api/Organization/{id}
[HttpDelete("csp/organization/byid/{id}")]
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
    /// Delete an organization using its Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
[HttpDelete("csp/organization/byname/{name}")]
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