using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> RegisterAsync(RegisterDto register)
    {
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = register.username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }
}