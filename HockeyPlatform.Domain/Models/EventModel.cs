namespace HockeyPlatform.Domain.Models;

public class EventModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price  { get; set; }
    public DateTime DeadlineTime { get; set; }
    public int MinCountPlayers { get; set; }
    public List<UserModel> PlayersList { get; set; }
}