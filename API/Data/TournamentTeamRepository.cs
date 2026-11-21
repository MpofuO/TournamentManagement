using API.Entities;
using API.Interfaces;

namespace API.Data;

public class TournamentTeamRepository : BaseRepository<TournamentTeam>, ITournamentTeamRepository
{
    public TournamentTeamRepository(AppDbContext context) : base(context) { }
}
