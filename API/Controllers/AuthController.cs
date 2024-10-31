//Handles user sign-up, login, and JWT generation

using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ETContext context) : ControllerBase
{
    [HttpPost("register")] //post : api/auth/register
    public async Task<ActionResult<User>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("username is taken");
        {
            
        }
        using var hmac = new HMACSHA3_512();

        var user = new User()
        {
            Username = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };
        
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    private async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()); 
    }
}
