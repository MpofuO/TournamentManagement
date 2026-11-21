using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MatchRepository : BaseRepository<Match>, IMatchRepository
{
    public MatchRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId)
    {
        return await context.Matches.Where(m => m.TournamentId == tournamentId).ToListAsync();
    }
}
