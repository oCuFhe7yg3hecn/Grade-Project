using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeProject.ProfileService.Infrastructure;
using GradeProject.ProfileService.Models;
using GradeProject.ProfileService.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeProject.ProfileService.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userServie)
        {
            _userService = userServie;
        }

        // GET: api/User
        [HttpGet]
        public async Task<List<User>> GetAsync() => await _userService.GetUsers();

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserInsertDTO newUser)
        {
            await _userService.CreateUser(newUser);
            return NoContent();
        }

        // PUT: api/User/5
        //TODO : Change it by checking the users exists
        //--------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]UserInsertDTO user)
        {
            await _userService.UpdateUser(id, user);
            return NoContent();
        }

        [HttpPost("{userId}/friend/{friendId}")]
        public async Task<IActionResult> AddFriend(string userId, string friendId)
        {
            await _userService.AddFriend(userId, friendId);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
