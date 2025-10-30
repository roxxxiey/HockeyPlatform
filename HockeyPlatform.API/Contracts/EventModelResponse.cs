namespace HockeyPlatform.API.Contracts;

public record EventModelResponse(
    int Id,
    string Title,
    string Description,
    int Price,
    DateTime DeadlineTime,
    int MinCountPlayers,
    List<UserModelResponse> PlayersList);