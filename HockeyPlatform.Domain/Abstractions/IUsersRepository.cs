using HockeyPlatform.Domain.Models;

namespace HockeyPlatform.Domain.Abstractions;

public interface IUsersRepository
{
    Task<List<UserModel>> GetByEventAsync(int id);
    Task<UserModel?> GetAsync(int id);
    Task<bool> CreateAsync(bool adminRights,
        string name,
        string surname,
        string patronymic,
        int balance,
        string playingPosition,
        int photo);
    Task<UserModel?> UpdateAsync(int id, bool adminRights,
        string name,
        string surname,
        string patronymic,
        int balance,
        string playingPosition,
        int photo);
    Task<bool> DeleteAsync(int id);
}