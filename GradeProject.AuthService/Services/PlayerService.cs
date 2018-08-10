using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using GradeProject.AuthService.Communication;
using GradeProject.AuthService.Extensions;
using GradeProject.AuthService.Infrastructure;
using GradeProject.AuthService.Models;
using GradeProject.AuthService.Models.Account.Register;
using Microsoft.AspNetCore.Http;

namespace GradeProject.AuthService.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly HttpClient _client;
        private readonly IEventBus _eventBus;

        public PlayerService(
            IUserRepository userRepo,
            IMapper mapper,
            IEventBus eventBus)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _eventBus = eventBus;
            _client = new HttpClient();
        }

        public async Task RegisterProfileAsync(PlayerRegisteModel userRegModel)
        {
            var profile = _mapper.Map<ProfileRegisterModel>(userRegModel);

            _eventBus.SendProfile(profile);

            //var res = await _client.PostAsJsonAsync(API.Profiles.RegisterProfile("https://localhost:44312"), profile);

            //if (res.StatusCode != System.Net.HttpStatusCode.Created) { throw new Exception(res.RequestMessage.Content.ToString()); }
        }

        public async Task RegisterUserAsync(PlayerRegisteModel userRegModel)
        {
            var user = _mapper.Map<User>(userRegModel);
            await _userRepo.AddAsync(user);
        }
    }
}
