namespace HockeyPlatform.Domain.Models;

public class UserModel
{
    public int Id { get; set; }
    public bool AdminRights { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public int Balance { get; set; }
    public string PlayingPosition { get; set; }
    public int Photo { get; set; }
}