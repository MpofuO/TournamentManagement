using API.Entities;

namespace API.Interfaces;

public interface ITournamentRepository: IBaseRepository<Tournament>
{
    Task<Tournament?> GetWithTeamsAsync(int tournamentId);
    Task<IEnumerable<Tournament>> GetByStatusAsync(TournamentStatus status);
    Task<IEnumerable<Tournament>> GetAllWithTeamsAsync();
}
