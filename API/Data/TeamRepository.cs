using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class TeamRepository : BaseRepository<Team>, ITeamRepository
{
    public TeamRepository(AppDbContext context) : base(context) { }

    public async Task<Team?> GetByNameAsync(string name)
    {
        return await context.Teams.FirstOrDefaultAsync(t => t.TeamName == name);
    }

    public async Task<Team?> GetByAbbreviationAsync(string abbr)
    {
        return await context.Teams.FirstOrDefaultAsync(t => t.Abbreviation == abbr);
    }

    public async Task<IEnumerable<Team>> GetByTournamentAsync(int tournamentId)
    {
        return await context.Teams
            .Include(t => t.TournamentTeams)
            .Where(t => t.TournamentTeams.Any(tt => tt.TournamentId == tournamentId))
            .ToListAsync();
    }
}