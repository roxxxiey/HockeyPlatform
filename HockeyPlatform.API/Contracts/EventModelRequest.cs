using HockeyPlatform.Domain.Models;

namespace HockeyPlatform.API.Contracts;

public record EventModelRequest(
    string Title,
    string Description,
    int Price,
    DateTime DeadlineTime,
    int MinCountPlayers,
    List<int> PlayerIds);