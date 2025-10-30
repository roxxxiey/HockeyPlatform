using HockeyPlatform.API.Contracts;
using HockeyPlatform.Domain.Abstractions;
using HockeyPlatform.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace HockeyPlatform.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _usersRepository;
    
    public  UsersController(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    [HttpGet("{id}/list-users")]
    public async Task<List<UserModel>> GetByEventIdAsync(int id)
    {
        var users = await _usersRepository.GetByEventAsync(id);
        
        return users;
    }

    [HttpGet("{id}")]
    public async Task<UserModel?> GetUser(int id)
    {
        var user = await _usersRepository.GetAsync(id);
        
        return user;
    }

    [HttpPost]
    public async Task<bool> CreateUser([FromBody] UserModelRequest request)
    {
        var result = await _usersRepository.CreateAsync(
            request.AdminRights, 
            request.Name, 
            request.Surname, 
            request.Patronymic, 
            request.Balance, 
            request.PlayingPosition, 
            request.Photo);
        
        return result;
    }

    [HttpPut("{id}")]
    public async Task<UserModel?> UpdateUser(int id, [FromBody] UserModelRequest request)
    {
        var result = await _usersRepository.UpdateAsync(
            id, 
            request.AdminRights, 
            request.Name, 
            request.Surname, 
            request.Patronymic, 
            request.Balance, 
            request.PlayingPosition, 
            request.Photo);
        
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<bool> DeleteUser(int id)
    {
        var result = await _usersRepository.DeleteAsync(id);
        
        return result;
    }
}