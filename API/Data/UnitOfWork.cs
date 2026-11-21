using API.Interfaces;

namespace API.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Tournaments = new TournamentRepository(context);
        Teams = new TeamRepository(context);
        TournamentTeams = new TournamentTeamRepository(context);
        Matches = new MatchRepository(context);
        MatchTeams = new MatchTeamRepository(context);
    }

    public ITournamentRepository Tournaments { get; }
    public ITeamRepository Teams { get; }
    public ITournamentTeamRepository TournamentTeams { get; }
    public IMatchRepository Matches { get; }
    public IMatchTeamRepository MatchTeams { get; }

    public async Task<bool> CompleteAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}
