namespace API.Interfaces;

public interface IUnitOfWork
{
    ITournamentRepository Tournaments { get; }
    ITeamRepository Teams { get; }
    ITournamentTeamRepository TournamentTeams { get; }
    IMatchRepository Matches { get; }
    IMatchTeamRepository MatchTeams { get; }
    Task<bool> CompleteAsync();
}
