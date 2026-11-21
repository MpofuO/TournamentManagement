using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MatchTeamRepository : BaseRepository<MatchTeam>, IMatchTeamRepository
{
    public MatchTeamRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<MatchTeam>> GetByTeamAsync(int teamId)
    {
        return await context.MatchTeams.Where(mt => mt.TeamID == teamId).ToListAsync();
    }
}
