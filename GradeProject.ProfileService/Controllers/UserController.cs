using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GradeProject.ProfileService.Infrastructure;
using GradeProject.ProfileService.Infrastructure.Services;
using GradeProject.ProfileService.Models;
using GradeProject.ProfileService.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeProject.ProfileService.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    // !!!!!!!!!!!! Players for Auth Service
    public class UsersController : Controller
    {
        private readonly IUserService _userSvc;

        public UsersController(IUserService userServie)
        {
            _userSvc = userServie;
        }

        // GET: api/User
        [HttpGet]
        [EnableCors("AllowAll")]
        public async Task<List<User>> GetAsync() => await _userSvc.GetUsersAsync();

        [HttpGet]
        [Route("getShortInfo/{id}")]
        [EnableCors("AllowAll")]
        public async Task<IActionResult> GetShortInfo(string id)
        {
            var user = await _userSvc.GetUserByIdAsync(id);
            return Ok(new
            {
                Id = user.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                NickName = user.FirstName
            });
        }


        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _userSvc.GetUserByIdAsync(id);
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserInsertDTO newUser)
        {
            await _userSvc.CreateUser(newUser);
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]UserInsertDTO user)
        {
            await _userSvc.UpdateUser(id, user);
            return NoContent();
        }

        [HttpPost("{userId}/friend/{friendId}")]
        public async Task<IActionResult> AddFriend(string userId, string friendId)
        {
            await _userSvc.AddFriend(userId, friendId);
            return Ok(_userSvc.GetUserByIdAsync(userId));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userSvc.DeleteUser(id);
            return NoContent();
        }
    }
}
