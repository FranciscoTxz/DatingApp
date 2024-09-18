using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> RegisterAsync(RegisterRequest request)
    {   
        if (await UserExistsAsync(request.username))
        {
            return BadRequest("Username already exists");
        }

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = request.username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    private async Task<bool> UserExistsAsync(string username) =>
        await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
}