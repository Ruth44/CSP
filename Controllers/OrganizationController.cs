using System.Collections.Generic;
using AutoMapper;
using CSP.Data;
using CSP.ViewModels;
using Microsoft.AspNetCore.Mvc;

using CSP.Models;

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
    public ActionResult <Organization> CreateOrganization(Organization org){
        
        _repository.CreateOrg(org);
        _repository.SaveChanges();

        return Ok("Successfully inserted "+ org.Name);
    }
        /// <summary>
        /// Update an organization using their ID
        /// </summary>
        /// <param name="orgUpdate"></param>
        /// <returns></returns>
    //PUT api/CSP/{id}
    [HttpPut("/UpdateById/{id}")]
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
    [HttpPut("/UpdatebyName/{organization_name}")]
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
[HttpDelete("/DeleteById/{id}")]
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
[HttpDelete("/DeletebyName/{name}")]
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