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
        [HttpPost]
        [Route("CreateAccount")]
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
               /// Update an account
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
    
    [HttpPatch("/UpdateUser/{username}")]
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

    }
}