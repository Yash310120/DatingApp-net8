using DATINGAPP.Data;
using DATINGAPP.DTOs;
using DATINGAPP.entities;
using DATINGAPP.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DATINGAPP.Controllers
{
    public class AccountController(DataContext context , ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")] // account//register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                Username = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key

            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.Username,
                Token = tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>Login(LoginDTO loginDTO)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == loginDTO.Username.ToLower());

            if (user == null) return Unauthorized("Inavlid username ");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            for (int i = 0; i < computedHash.Length; i++)
            { 
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Inavlid password");
            }

            return new UserDto
            {
                Username = user.Username,
                Token = tokenService.CreateToken(user)
            };
        }


        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()); 
        }

    }

}