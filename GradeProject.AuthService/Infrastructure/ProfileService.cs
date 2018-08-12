using GradeProject.AuthService.Models;
using GradeProject.AuthService.Models.Account;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Infrastructure
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepo;

        public ProfileService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = _userRepo.FindBySubjectId(context.Subject.GetSubjectId()).SubjectId;
            var client = new HttpClient();
            var userInfo = await client.GetStringAsync($"https://localhost:44312/api/Players/getShortInfo/{userId}");
            var user = JsonConvert.DeserializeObject<ProfileInfo>(userInfo);

            context.IssuedClaims.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            context.IssuedClaims.Add(new Claim(ClaimTypes.Role, user.Role ?? "Player"));
            context.IssuedClaims.Add(new Claim("FirstName", user.FirstName));
            context.IssuedClaims.Add(new Claim("LastName", user.LastName));
            context.IssuedClaims.Add(new Claim("NickName", user.NickName));
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var isActive = _userRepo.FindBySubjectId(context.Subject.GetSubjectId()).IsActive;
            context.IsActive = isActive;

            return Task.FromResult(0);
        }
    }
}
