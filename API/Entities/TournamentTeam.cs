namespace API.Entities;

public class TournamentTeam
{
    public int TournamentTeamId { get; set; }

    public int TournamentId { get; set; }
    public Tournament Tournament { get; set; } = null!;
    public int TeamID { get; set; }
    public Team Team { get; set; } = null!;
}
