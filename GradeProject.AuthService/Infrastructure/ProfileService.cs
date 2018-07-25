using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _userRepo.FindBySubjectId(context.Subject.GetSubjectId());
            context.IssuedClaims.Add(new Claim(ClaimTypes.GivenName, user.Email));

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var isActive = _userRepo.FindBySubjectId(context.Subject.GetSubjectId()).IsActive;
            context.IsActive = isActive;

            return Task.FromResult(0);
        }
    }
}
