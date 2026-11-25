using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TeamsController(IUnitOfWork uow) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Team>>> GetAll()
    {
        var teams = await uow.Teams.GetAllAsync();
        return Ok(teams);
    }

    [HttpGet("{tournamentId:int}")]
    public async Task<ActionResult<IEnumerable<Team>>> GetTournamentTeams(int tournamentId)
    {
        var teams = await uow.Teams.GetByTournamentAsync(tournamentId);
        return Ok(teams);
    }

    [HttpPost]
    public async Task<ActionResult<Team>> CreateTeam(TeamDto teamDto)
    {
        var existingTeam = await uow.Teams.GetByNameAsync(teamDto.TeamName);
        if (existingTeam != null)
        {
            return BadRequest("A team with the same name already exists.");
        }

        existingTeam = await uow.Teams.GetByAbbreviationAsync(teamDto.Abbreviation);
        if (existingTeam != null)
        {
            return BadRequest("A team with the same abbreviation already exists.");
        }

        await uow.Teams.CreateAsync(new Team
        {
            TeamName = teamDto.TeamName,
            Abbreviation = teamDto.Abbreviation
        });

        if(await uow.CompleteAsync())
        {
            return Ok(teamDto);
        }
        return BadRequest("Failed to create team.");
    }

    [HttpPost("bulk/preview")]
    public async Task<ActionResult<IEnumerable<Team>>> PreviewBulkCreateTeams(BulkTeamDto bulkTeamDto)
    {
        var existingTeams = new List<Team>();
        foreach (var teamDto in bulkTeamDto.Teams)
        {
            var existingTeamByName = await uow.Teams.GetByNameAsync(teamDto.TeamName);
            if (existingTeamByName != null)
            {
                existingTeams.Add(existingTeamByName);
                continue;
            }

            var existingTeamByAbbr = await uow.Teams.GetByAbbreviationAsync(teamDto.Abbreviation);
            if (existingTeamByAbbr != null)
            {
                existingTeams.Add(existingTeamByAbbr);
                continue;
            }
        }

        return Ok(existingTeams);
    }

    [HttpPost("bulk")]
    public async Task<ActionResult> BulkCreateTeams(BulkTeamDto bulkTeamDto)
    {
        foreach (var teamDto in bulkTeamDto.Teams)
        {
            var existingTeamByName = await uow.Teams.GetByNameAsync(teamDto.TeamName);
            if (existingTeamByName != null)
            {
                return BadRequest($"A team with the name '{teamDto.TeamName}' already exists.");
            }

            var existingTeamByAbbr = await uow.Teams.GetByAbbreviationAsync(teamDto.Abbreviation);
            if (existingTeamByAbbr != null)
            {
                return BadRequest($"A team with the abbreviation '{teamDto.Abbreviation}' already exists.");
            }

            await uow.Teams.CreateAsync(new Team
            {
                TeamName = teamDto.TeamName,
                Abbreviation = teamDto.Abbreviation
            });
        }

        if(await uow.CompleteAsync())
        {
            return Ok("Teams created successfully.");
        }
        return BadRequest("Failed to create teams.");
    }

    [HttpPatch("{teamId:int}")]
    public async Task<ActionResult> UpdateTeam(int teamId, UpdateTeamDto teamDto)
    {
        var team = await uow.Teams.GetByIdAsync(teamId);
        if(team is null) return NotFound("Team not found.");

        team.TeamName = teamDto.TeamName ?? team.TeamName;
        team.Abbreviation = teamDto.Abbreviation ?? team.Abbreviation;

        if(await uow.CompleteAsync())
        {
            return Ok("Team updated successfully.");
        }
        return BadRequest("Failed to update team.");

    }

    [HttpDelete("{teamId:int}")]
    public async Task<ActionResult> DeleteTeam(int teamId)
    {
        var team = await uow.Teams.GetByIdAsync(teamId);
        if (team == null)
        {
            return NotFound("Team not found.");
        }

        await uow.Teams.DeleteAsync(team);

        if(await uow.CompleteAsync())
        {
            return Ok("Team deleted successfully.");
        }
        return BadRequest("Failed to delete team.");
    }
}
