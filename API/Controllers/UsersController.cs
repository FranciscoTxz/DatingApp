using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _repository;

    public UsersController(IUserRepository repository)
    {
        _repository = repository;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();

        return Ok(users);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AppUser>>GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
    
        if (user == null) return NotFound();
    
        return user;
    }

    [HttpGet("{username}")]
public async Task<ActionResult<AppUser>>GetByUsernameAsync(string username)
    {
        var user = await _repository.GetByUsernameAsync(username);
    
        if (user == null) return NotFound();
    
        return user;
    }
}