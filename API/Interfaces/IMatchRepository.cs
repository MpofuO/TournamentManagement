using API.Entities;

namespace API.Interfaces;

public interface IMatchRepository : IBaseRepository<Match>
{
    Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId);
    
}
