//Handles user sign-up, login, and JWT generation

using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ETContext context, ITokenService tokenService) : ControllerBase
{
    [HttpPost("register")] //post : api/auth/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("username is taken");
        
        using var hmac = new HMACSHA3_512();

        var user = new User()
        {
            Username = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key //I'll use this key in the login to hash the entered password and compare "hashedPasswords"
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
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Username == loginDto.Username.ToLower());
        
        if (user == null) return Unauthorized("invalid username or password");
        
        using var hmac = new HMACSHA3_512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Incorrect password");            
        }
        
        return new UserDto
        {
            Username = user.Username,
            Token = tokenService.CreateToken(user) // Returns JWT to client on successful login
        };
    }
    
    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()); 
    }
}
