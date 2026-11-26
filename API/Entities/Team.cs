using API.Helpers;

namespace API.Entities;

public class Team
{
    private string _teamName = null!;
    private string _abbreviation = null!;

    public int TeamID { get; set; }
    public string TeamName
    {
        get => _teamName;
        set
        {
            _teamName = TeamNameHelper.Clean(value);
            NormalizedTeamName = TeamNameHelper.Key(_teamName);
        }
    }
    public string NormalizedTeamName { get; private set; } = null!;
    public string Abbreviation
    {
        get => _abbreviation;
        set => _abbreviation = value.ToUpperInvariant();
    }

    public ICollection<TournamentTeam> TournamentTeams = [];
    public ICollection<MatchTeam> MatchTeams = [];
}
