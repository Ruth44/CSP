using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSP.Data;
using CSP.Models;
using CSP.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CSP.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController: ControllerBase
    {
         private readonly IUserRepo _userService;
         private readonly IMapper _mapper;

        public UserController(IUserRepo userService, IMapper mapper)
        {
            _mapper=mapper;
            _userService = userService;
        }
        /// Create Account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("csp/user")]
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

            /// <summary>
        /// Get all requests
        /// </summary>
        /// <returns></returns>
        [HttpGet("csp/users")]
      public ActionResult <IEnumerable<User>> GetAllUsers()
      {
          var reqItems = _userService.GetAllAsync();
        //    var requests= _mapper.Map<IEnumerable<CreateUserAccount>>(reqItems);


               
          return Ok(reqItems);
      }
               /// Update an account
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
    
    [HttpPatch("/csp/user/{username}")]
    public ActionResult PartialOrganizationUpdate(string username, JsonPatchDocument<CreateUserAccount> patchDoc)
{
      var orgModelFromRepo = _userService.GetUserByName(username);
        if(orgModelFromRepo == null){
            return NotFound("User Not found");
        }
        var orgToPatch = _mapper.Map<CreateUserAccount>(orgModelFromRepo);
        patchDoc.ApplyTo(orgToPatch, ModelState);
        if(!TryValidateModel(orgToPatch))
        {
            return ValidationProblem(ModelState);
        }
      _mapper.Map(orgToPatch, orgModelFromRepo);
      _userService.UpdateUser(orgModelFromRepo);
      _userService.SaveChanges();
      return Ok("Updated Successfully");

}
  /// <summary>
        /// Get a User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    //   GET api/songs/$
      [HttpGet("csp/user/{id}")]
      public ActionResult <CreateUserAccount> GetUserById(int id)
      {
          var reqItem = _userService.GetUserById(id);
          if(reqItem==null){
               return NotFound("The User could not be found.");
          }
          var getReq=_mapper.Map<CreateUserAccount>(reqItem);
      
          return Ok(getReq);
          
      }
      /// <summary>
        /// Delete a Ticket using its Ticket Number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
// DELETE api/Organization/{id}
[HttpDelete("/csp/user/{userid}")]
public ActionResult DeleteUserbyId(int id)
{
    var toBeDeleted= _userService.GetUserById(id);
  if(toBeDeleted==null){
      return NotFound("User not found!");
  }
        _userService.DeleteUser(toBeDeleted);
    _userService.SaveChanges();
      
    return Ok("Deleted Successfully");
}
    }
}