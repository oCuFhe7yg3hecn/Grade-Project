﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GradeProject.ProfileService.Infrastructure;
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
    public class UsersController : Controller
    {
        private readonly UserService _userSvc;

        public UsersController(UserService userServie)
        {
            _userSvc = userServie;
        }

        //[HttpGet]
        //[Authorize]
        //public IActionResult GetCalims()
        //{
        //    return new JsonResult(User.Claims.Select(x => new { x.Type, x.Value }));
        //}

        // GET: api/User
        [HttpGet]
        [EnableCors("AllowAll")]
        [Authorize]
        public async Task<List<User>> GetAsync() => await _userSvc.GetUsers();

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return _userSvc
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserInsertDTO newUser)
        {
            await _userSvc.CreateUser(newUser);
            return NoContent();
        }

        // PUT: api/User/5
        //TODO : Change it by checking the users exists
        //--------------------------------------------
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
            return NoContent();
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
