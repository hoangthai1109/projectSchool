using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UserControllers: baseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        public UserControllers(IUserRepository userRepository, IMapper mapper, IImageService imageService) {
            _userRepository = userRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUser() {
            var users = await _userRepository.GetUserAsync();

            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpPost("get-user-by-name")]
        [Authorize]
        public async Task<ActionResult<MemberDto>> GetUser(string userName) {
            var user = await _userRepository.GetMemberAsync(userName);

            return _mapper.Map<MemberDto>(user);
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult<AppUser>> UpdateUser(AppUser appUser) {
            var user = await _userRepository.GetUserByIdAsync(appUser.id);

            if (user == null) return BadRequest("Cannot find user");

            _userRepository.Update(appUser);

            return await _userRepository.SaveAsync(appUser);
        }
    }
}