namespace API.Entities;

[Flags]
public enum TournamentStatus { SettingUp = 1, Upcoming = 2, Ongoing = 4, Completed = 8 }
public class Tournament
{
    public int TournamentId { get; set; }
    public string TournamentName { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; } = null!;
    public TournamentStatus Status { get; set; }

    public ICollection<TournamentTeam> TournamentTeams { get; set; } = [];
    public ICollection<Match> Matches { get; set; } = [];
}
