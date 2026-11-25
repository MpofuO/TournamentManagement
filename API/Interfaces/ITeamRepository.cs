using API.Entities;

namespace API.Interfaces;

public interface ITeamRepository : IBaseRepository<Team>
{
    Task<Team?> GetByNameAsync(string name);
    Task<Team?> GetByAbbreviationAsync(string abbr);
    Task<IEnumerable<Team>> GetByTournamentAsync(int tournamentId);
}
