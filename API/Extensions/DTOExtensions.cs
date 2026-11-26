using API.DTOs;
using API.Entities;

namespace API.Extensions;

public static class DTOExtensions
{
    public static Team ToEntity(this TeamDto dto)
    {
        return new Team
        {
            TeamName = dto.TeamName,
            Abbreviation = dto.Abbreviation
        };
    }

    public static TeamDto ToDto(this Team entity)
    {
        return new TeamDto
        {
            TeamId = entity.TeamID,
            TeamName = entity.TeamName,
            Abbreviation = entity.Abbreviation
        };
    }
}
