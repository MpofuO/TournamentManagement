using API.Entities;

namespace API.Interfaces;

public interface ITeamRepository : IBaseRepository<Team>
{
    Task<Team?> GetByNameAsync(string name);
    Task<IEnumerable<Team>> GetByTournamentAsync(int tournamentId);
}
