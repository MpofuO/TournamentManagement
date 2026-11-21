using API.Entities;

namespace API.Interfaces;

public interface IMatchTeamRepository : IBaseRepository<MatchTeam>
{
    Task<IEnumerable<MatchTeam>> GetByTeamAsync(int teamId);
}
