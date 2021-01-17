using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;

        public ITokenService _tokenService { get; }

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser()
            {
                Username = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

           return new UserDto
            {
                Username=user.Username,
                Token=_tokenService.CreateToken(user)
            };        


        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
           // if(! await UserExists(loginDto.Username)) return BadRequest("User name does not exist");
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == loginDto.Username);
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            return new UserDto { 
             Username=loginDto.Username,
             Token=_tokenService.CreateToken(user)
            };
            
        }

        private Task<bool> UserExists(string username)
        {
            return _context.Users.AnyAsync(x => x.Username==username.ToLower());
        }
    }
}
