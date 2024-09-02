using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>>GetUsers()
    {
        var users = await _context.Users.ToListAsync();

        return users;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>>GetUsersById(int id)
    {
        var user = await _context.Users.FindAsync(id);
    
        if (user == null) return NotFound();
    
        return user; //ver si jala
    }


    [HttpGet("name/{name}")]
    public ActionResult<string> Ready(string name)
    {
        return $"Hello, {name}";
    }

    [HttpPost]
    public async Task<ActionResult<AppUser>> AddUser(AppUser user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsersById), new { id = user.Id }, user);
    }


}