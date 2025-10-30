using HockeyPlatform.Domain.Abstractions;
using HockeyPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HockeyPlatform.Persistence.Repositories;

public class EventsRepository : IEventsRepository
{
    private readonly HockeyPlatformDbContext _hockeyPlatformDbContext;

    public EventsRepository(HockeyPlatformDbContext hockeyPlatformDbContext)
    {
        _hockeyPlatformDbContext = hockeyPlatformDbContext;
    }

    public async Task<List<EventModel>> GetAllAsync()
    {
        var events = await _hockeyPlatformDbContext.Events.ToListAsync();
        
        var eventPlayers = await _hockeyPlatformDbContext.EventUsers
            .Join(_hockeyPlatformDbContext.Users, 
                eu => eu.UserId, 
                u => u.Id, 
                (eu, u) => new { eu.EventId, User = u})
            .ToListAsync();
        
        var groupedPlayers = eventPlayers
            .GroupBy(eu => eu.EventId)
            .ToDictionary(g => g.Key, 
                g => g.Select(x => x.User)
                .ToList());
        
        foreach (var eventModel in events)
        {
            groupedPlayers.TryGetValue(eventModel.Id, out var players);
            eventModel.PlayersList = players ?? new List<UserModel>();
        }
        
        return events;
    }

    public async Task<EventModel?> GetAsync(int id)
    {
        var eventModel = await _hockeyPlatformDbContext.Events.FindAsync(id);

        if (eventModel == null) return null;
        
        var eventPlayers = await _hockeyPlatformDbContext.EventUsers
            .Where(eu => eu.EventId == id)
            .Join(_hockeyPlatformDbContext.Users,
                eu => eu.UserId,
                u => u.Id,
                (eu, u) => u)
            .ToListAsync();
        
        eventModel.PlayersList = eventPlayers;
        
        return eventModel;
    }

    public async Task<bool> CreateAsync(
        string title, 
        string description, 
        int price, 
        DateTime deadlineTime, 
        int minCountPlayers, 
        List<int> playerIds)
    {
        
        var playersList = await _hockeyPlatformDbContext.Users 
            .Where(u => playerIds.Contains(u.Id)).ToListAsync();

        var eventModel = new EventModel
        {
            Title = title,
            Description = description,
            Price = price,
            DeadlineTime = deadlineTime,
            MinCountPlayers = minCountPlayers,
            PlayersList = playersList
        };
        
        await _hockeyPlatformDbContext.Events.AddAsync(eventModel);
        var result = await _hockeyPlatformDbContext.SaveChangesAsync();

        if (result == 0) return false;
        
        foreach (var player in eventModel.PlayersList)
        {
            var eventUser = new EventUserModel
            {
                EventId = eventModel.Id,
                UserId = player.Id
            };
            await _hockeyPlatformDbContext.EventUsers.AddAsync(eventUser);
        }

        result += await _hockeyPlatformDbContext.SaveChangesAsync();
        
        return result > 0;
    }

    public async Task<EventModel?> UpdateAsync(
        int id,
        string title, 
        string description, 
        int price, 
        DateTime deadlineTime, 
        int minCountPlayers, 
        List<int> playerIds)
    {
        var existingEvent = await _hockeyPlatformDbContext.Events.FindAsync(id);
        if (existingEvent == null) return null;

        existingEvent.Title = title;
        existingEvent.Description = description;
        existingEvent.Price = price;
        existingEvent.DeadlineTime = deadlineTime;
        existingEvent.MinCountPlayers = minCountPlayers;

        var existingEventUsers = await _hockeyPlatformDbContext.EventUsers
            .Where(eu => eu.EventId == id)
            .ToListAsync();

        _hockeyPlatformDbContext.EventUsers.RemoveRange(existingEventUsers);
        
        var playersList = await _hockeyPlatformDbContext.Users 
            .Where(u => playerIds.Contains(u.Id)).ToListAsync();

        var newEventUsers = playersList.Select(u => new EventUserModel
        {
            EventId = id,
            UserId = u.Id
        });

        await _hockeyPlatformDbContext.EventUsers.AddRangeAsync(newEventUsers);

        await _hockeyPlatformDbContext.SaveChangesAsync();

        existingEvent.PlayersList = playersList;

        return existingEvent;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var eventModel = await _hockeyPlatformDbContext.Events.FindAsync(id);

        if (eventModel == null) return false;

        _hockeyPlatformDbContext.Events.Remove(eventModel);

        var result = await _hockeyPlatformDbContext.SaveChangesAsync();

        return result > 0;
    }
}