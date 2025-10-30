using HockeyPlatform.Domain.Models;

namespace HockeyPlatform.Domain.Abstractions;

public interface IUsersService
{
    Task<List<UserModel>> GetAllUsersByEventAsync(int id);
    Task<UserModel?> GetUserByIdAsync(int id);
    Task<bool> CreateUserAsync(
        bool adminRights,
        string name,
        string surname,
        string patronymic,
        int balance,
        string playingPosition,
        int photo);
    Task<UserModel?> UpdateUserAsync(
        int id, 
        bool adminRights,
        string name,
        string surname,
        string patronymic,
        int balance,
        string playingPosition,
        int photo);
    Task<bool> DeleteUserAsync(int id);
}