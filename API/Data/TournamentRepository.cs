using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class TournamentRepository : BaseRepository<Tournament>, ITournamentRepository
{
    public TournamentRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Tournament>> GetAllWithTeamsAsync()
    {
        return await context.Tournaments
            .Include(t => t.TournamentTeams)
            .ThenInclude(tt => tt.Team)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tournament>> GetByStatusAsync(TournamentStatus status)
    {
        return await context.Tournaments
            .Where(t => (t.Status & status) != 0)
            .Include(t => t.TournamentTeams)
            .ThenInclude(tt => tt.Team)
            .ToListAsync();
    }

    public async Task<Tournament?> GetWithTeamsAsync(int tournamentId)
    {
        return await context.Tournaments
            .Include(t => t.TournamentTeams)
            .ThenInclude(tt => tt.Team)
            .FirstOrDefaultAsync(t => t.TournamentId == tournamentId);
    }
}
