using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extension;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController: baseApiController
    {
        readonly DataContext _context;
        readonly ITokenService _token;
        readonly IUserRepository _userRepository;
        readonly IImageService _imageService;
        readonly IMapper _mapper;
        public AccountController(DataContext context, ITokenService token, IUserRepository userRepository, IImageService imageService, IMapper mapper) {
            _token = token;
            _context = context;
            _userRepository = userRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpPost("unau/register")] // {uri}/api/account/register?username=abc&password=cde
        public async Task<ActionResult<UserDtos>> Register([FromBody]RegisterDtos registerDtos) {
            try 
            {
            
            if (await this.UserExist(registerDtos.Username)) return BadRequest("Username is taken");
            
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                userName = registerDtos.Username.ToLower(),
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDtos.Password)),
                passwordSalt = hmac.Key,
                Gender = registerDtos.Gender,
                FullName = registerDtos.FullName,
                DateOfBirth = registerDtos.DateofBirth,
                Address = registerDtos.Address,
                PhoneNumber = registerDtos.PhoneNumber,
                Email = registerDtos.Email,
                CreateDate = DateTime.Now,
                RoleDefault = "user"
            };

            _context.appUsers.Add(user);
            await _context.SaveChangesAsync();
            
            // upload file for user register
            // var result = await _imageService.AddPhotoAsync(file);

            // var image = new image
            // {
            //     Url = result.SecureUrl.AbsoluteUri,
            //     PublicId = result.PublicId
            // };

            // image.IsMain = true;

            // user.Images.Add(image);

            return StatusCode(200, new UserDtos
            {
                Username = user.userName,
                Token = _token.CreateToken(user),
            });
            } catch (Exception e)  {
                throw(e);
            }
        }

        [HttpPost("unau/upload-file")]
        public async Task<ActionResult<ImageDto>> UploadFile(IFormFile file, string username) {
            var user = await _context.appUsers.SingleOrDefaultAsync(user => user.userName == username);

            var result = await _imageService.AddPhotoAsync(file);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var image = new image
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            image.IsMain = true;

            user.Images.Add(image);

            if (await _userRepository.SaveAllAsync())
            {
                return CreatedAtAction(nameof(Register), new {username = user.userName}, _mapper.Map<ImageDto>(image));
            }

            return BadRequest("Cannot upload file");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDtos>> Login(LoginDtos loginDtos) {
            var user = await _context.appUsers.SingleOrDefaultAsync(x => 
            x.userName == loginDtos.Username);

            if (user == null) return Unauthorized("invalid user");

            using var hmac = new HMACSHA512(user.passwordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDtos.Password));

            for (int i = 0; i < computedHash.Length; i++) {
                if (computedHash[i] != user.passwordHash[i]) return Unauthorized("Incorrect password! Please try again");
            }

            return new UserDtos
            {
                Username = user.userName,
                Token = _token.CreateToken(user)
            };
        }

        protected async Task<bool> UserExist(string username) {
            return await _context.appUsers.AnyAsync(x => x.userName == username.ToLower());
        }
    }
}