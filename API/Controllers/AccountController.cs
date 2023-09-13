

using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApicontroller
    {
        private readonly DataContext _context;
        public iTokenService _TokenService;
        public AccountController(DataContext context, iTokenService tokenService)
        {
            _TokenService = tokenService;
            _context = context;
        }
        [HttpPost("register")] //POST : api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.username))
            {
                return BadRequest("username is taken");
            }

            using var hmac = new HMACSHA512();
            var user = new Appuser
            {
                UserName = registerDto.username.ToLower(),
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                passwordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                Username = user.UserName,
                Token = _TokenService.CreateToken(user)
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto)

        {

            Console.WriteLine("Login attempt for username: " + loginDto.Password);
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("invalid username");
            using var hmac = new HMACSHA512(user.passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.passwordHash[i])
                {
                    return Unauthorized("invalid password");
                }
            }
            return new UserDto
            {
                Username = user.UserName,
                Token = _TokenService.CreateToken(user)

            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}