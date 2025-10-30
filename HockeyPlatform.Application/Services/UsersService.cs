using HockeyPlatform.Domain.Abstractions;
using HockeyPlatform.Domain.Models;

namespace HockeyPlatform.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    
    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<List<UserModel>> GetAllUsersByEventAsync(int id)
    {
        return await _usersRepository.GetByEventAsync(id);
    }

    public async Task<UserModel?> GetUserByIdAsync(int id)
    {
        return await _usersRepository.GetAsync(id);
    }

    public async Task<bool> CreateUserAsync(
        bool adminRights,
        string name,
        string surname,
        string patronymic,
        int balance,
        string playingPosition,
        int photo)
    {
        return await _usersRepository.CreateAsync(adminRights, name, surname, patronymic, 
            balance, playingPosition, photo);
    }

    public async Task<UserModel?> UpdateUserAsync(
        int id, 
        bool adminRights,
        string name,
        string surname,
        string patronymic,
        int balance,
        string playingPosition,
        int photo)
    {
        return await _usersRepository.UpdateAsync(id, adminRights, name, surname, 
            patronymic, balance, playingPosition, photo);
    }
    
    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _usersRepository.DeleteAsync(id);
    }
}