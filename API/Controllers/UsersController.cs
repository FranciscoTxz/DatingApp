using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using API.DTOs;

namespace API.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberResponse>>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();

        var response = _mapper.Map<IEnumerable<MemberResponse>>(users);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MemberResponse>>GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
    
        if (user == null) return NotFound();
    
        return _mapper.Map<MemberResponse>(user);
    }

    [HttpGet("{username}")]
public async Task<ActionResult<MemberResponse>>GetByUsernameAsync(string username)
    {
        var user = await _repository.GetByUsernameAsync(username);
    
        if (user == null) return NotFound();
    
        return _mapper.Map<MemberResponse>(user);
    }
}