namespace API.Entities;

public class MatchTeam
{
    public int MatchTeamId { get; set; }
    public int GoalsScored { get; set; }
    public bool IsWinner { get; set; }

    public int MatchId { get; set; }
    public Match Match { get; set; } = null!;
    public int TeamID { get; set; }
    public Team Team { get; set; } = null!;
}
