namespace API.Entities;

public class Team
{
    public int TeamID {get; set;}
    public string TeamName {get; set;} = null!;
    public string Abbreviation {get; set;} = null!;

    public ICollection<TournamentTeam> TournamentTeams = [];
    public ICollection<MatchTeam> MatchTeams = [];
}
