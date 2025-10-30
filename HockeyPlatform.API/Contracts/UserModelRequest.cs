namespace HockeyPlatform.API.Contracts;

public record UserModelRequest(
    bool AdminRights, 
    string Name, 
    string Surname, 
    string Patronymic,
    int Balance,
    string PlayingPosition,
    int Photo);