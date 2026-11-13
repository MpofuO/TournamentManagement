namespace API.Entities;

public class Match
{
    public int MatchId { get; set; }
    public DateTime MatchDate { get; set; }

    public int TournamentId { get; set; }
    public Tournament Tournament { get; set; } = null!;

    public ICollection<MatchTeam> MatchTeams = [];
}
