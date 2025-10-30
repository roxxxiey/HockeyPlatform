using HockeyPlatform.Domain.Abstractions;
using HockeyPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HockeyPlatform.Persistence.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly HockeyPlatformDbContext _hockeyPlatformDbContext;

    public UsersRepository(HockeyPlatformDbContext hockeyPlatformDbContext)
    {
        _hockeyPlatformDbContext = hockeyPlatformDbContext;
    }

    public async Task<List<UserModel>> GetByEventAsync(int id)
    {
        var users = await _hockeyPlatformDbContext.EventUsers
            .Where(eu => eu.EventId == id)
            .Join(
                _hockeyPlatformDbContext.Users,
                eu => eu.UserId,
                u => u.Id,
                (eu, u) => u
            )
            .ToListAsync();

        return users;
    }

    public async Task<UserModel?> GetAsync(int id)
    {
        var user = await _hockeyPlatformDbContext.Users.FindAsync(id);

        return user;
    }

    public async Task<bool> CreateAsync(bool adminRights, 
        string name, 
        string surname, 
        string patronymic,
        int balance,
        string playingPosition,
        int photo)
    {
        var user = new UserModel
        {
            AdminRights = adminRights,
            Name = name,
            Surname = surname,
            Patronymic = patronymic,
            Balance = balance,
            PlayingPosition = playingPosition,
            Photo = photo
        };
        
        await _hockeyPlatformDbContext.Users.AddAsync(user);
        
        var result = await _hockeyPlatformDbContext.SaveChangesAsync();
        
        return result > 0;
    }

    public async Task<UserModel?> UpdateAsync(int id, bool adminRights,
        string name,
        string surname,
        string patronymic,
        int balance,
        string playingPosition,
        int photo)
    {
        var existingUser = await _hockeyPlatformDbContext.Users.FindAsync(id);

        if (existingUser == null) return null;

        existingUser.AdminRights = adminRights;
        existingUser.Name = name;
        existingUser.Surname = surname;
        existingUser.Patronymic = patronymic;
        existingUser.Balance = balance;
        existingUser.PlayingPosition = playingPosition;
        existingUser.Photo = photo;
        
        await _hockeyPlatformDbContext.SaveChangesAsync();
        return existingUser;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _hockeyPlatformDbContext.Users.FindAsync(id);

        if (user == null) return false;

        _hockeyPlatformDbContext.Users.Remove(user);

        var result = await _hockeyPlatformDbContext.SaveChangesAsync();

        return result > 0;
    }
}