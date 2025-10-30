namespace HockeyPlatform.API.Contracts;

public record UserModelResponse(
    int Id, 
    bool AdminRights, 
    string Name, 
    string Surname, 
    string Patronymic,
    int Balance,
    string PlayingPosition,
    int Photo);