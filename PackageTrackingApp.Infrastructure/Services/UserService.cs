﻿using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using PackageTrackingApp.Core.Domains;
using PackageTrackingApp.Core.Repositories;
using PackageTrackingApp.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PackageTrackingApp.Core.Domain;
using Microsoft.Extensions.Logging;

namespace PackageTrackingApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = _userRepository.GetUserByIdAsync(userId);

            if(user is null)
            {
                throw new Exception($"User with id {userId} doesn't exist.");
            }

            await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<UserDto> GetUserAsync(Guid userId)
            => _mapper.Map<UserDto>(await _userRepository.GetUserByIdAsync(userId));

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
            => _mapper.Map<IEnumerable<UserDto>>(await _userRepository.GetAllUsersAsync());

        public async Task LoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> RegisterAsync(string email, string login, string password,
            string confirmPassword)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if(user is not null)
            {
                throw new Exception($"Email {email} is already taken.");
            }

            user = await _userRepository.GetUserByLoginAsync(login);
            if(user is not null) 
            {
                throw new Exception($"Login {login} is already taken.");
            }

            var hashedPassword = BCryptNet.HashPassword(password);
            user = new User(email, login, Roles.User, password, confirmPassword);

            await _userRepository.AddUserAsync(user);

            return user.Guid;
        }

        public async Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}