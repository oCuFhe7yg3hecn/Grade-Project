﻿using AutoMapper;
using GradeProject.ProfileService.Models;
using GradeProject.ProfileService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Infrastructure
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DefaultAvatarsFactory _avatarsFactory;

        public UserService(UserRepository userRepository, IMapper mapper, DefaultAvatarsFactory avatarsFactory)
        {
            this._userRepository = userRepository;
            _mapper = mapper;
            _avatarsFactory = avatarsFactory;
        }

        public async Task<List<User>> GetUsers() =>
            await _userRepository.GetUsers(_ => true);

        public async Task CreateUser(UserInsertDTO newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.ImageURL = _avatarsFactory.GetRandomAvatar(user.Gender);
            await _userRepository.Insert(user);
        }
             
        public async Task UpdateUser(string id, UserInsertDTO newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.Id = Guid.Parse(id);
            user.ImageURL = _avatarsFactory.GetRandomAvatar(user.Gender);
            await _userRepository.Update(user);
        }

        public async Task AddFriend(string userId, string friendId)
        {
            await _userRepository.AddFriend(userId, friendId);
        }

        public async Task DeleteUser(string id) => _userRepository.Delete(id);
    }
}