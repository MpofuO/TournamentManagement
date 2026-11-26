using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class TeamsController(IUnitOfWork uow) : BaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamDto>>> GetAll()
    {
        var teams = await uow.Teams.GetAllAsync();
        return Ok(teams.Select(t => t.ToDto()));
    }

    [HttpGet("{tournamentId:int}")]
    public async Task<ActionResult<IEnumerable<TeamDto>>> GetTournamentTeams(int tournamentId)
    {
        var teams = await uow.Teams.GetByTournamentAsync(tournamentId);
        return Ok(teams.Select(t => t.ToDto()));
    }

    [HttpPost]
    public async Task<ActionResult<TeamDto>> CreateTeam(TeamDto teamDto)
    {
        var existingTeam = await uow.Teams.GetByNameAsync(teamDto.TeamName);
        if (existingTeam != null)
            return BadRequest("A team with the same name already exists.");

        existingTeam = await uow.Teams.GetByAbbreviationAsync(teamDto.Abbreviation);
        if (existingTeam != null)
            return BadRequest("A team with the same abbreviation already exists.");

        var newTeam = teamDto.ToEntity();
        await uow.Teams.CreateAsync(newTeam);

        try
        {
            if (await uow.CompleteAsync())
            {
                return Ok(newTeam.ToDto());
            }
            return BadRequest("Failed to create team.");
        }
        catch (DbUpdateException exception) when (IsUniqueConstraintViolation(exception))
        {
            return Conflict("A team with the same name or abbreviation already exists.");
        }
    }

    [HttpPost("bulk/preview")]
    public async Task<ActionResult<IEnumerable<TeamDto>>> PreviewBulkCreateTeams(BulkTeamDto bulkTeamDto)
    {
        var existingTeams = new List<TeamDto>();
        foreach (var item in bulkTeamDto.Teams)
        {
            string[] parts = item.NameAbbreviation.Split('-');
            if (parts.Length != 2)
                return BadRequest($"Invalid format for team: {item.NameAbbreviation}. Expected format: 'Team Name-ABC'.");

            string teamName = parts[0].Trim();
            var existingTeamByName = await uow.Teams.GetByNameAsync(teamName);
            if (existingTeamByName != null && !existingTeams.Any(t => t.TeamId == existingTeamByName.TeamID))
            {
                existingTeams.Add(existingTeamByName.ToDto());
                continue;
            }

            string abbreviation = parts[1].Trim();
            var existingTeamByAbbr = await uow.Teams.GetByAbbreviationAsync(abbreviation);
            if (existingTeamByAbbr != null && !existingTeams.Any(t => t.TeamId == existingTeamByAbbr.TeamID))
            {
                existingTeams.Add(existingTeamByAbbr.ToDto());
                continue;
            }
        }

        return Ok(existingTeams);
    }

    [HttpPost("bulk")]
    public async Task<ActionResult> BulkCreateTeams(BulkTeamDto bulkTeamDto)
    {
        var teams = new List<Team>();

        foreach (var item in bulkTeamDto.Teams)
        {
            string[] parts = item.NameAbbreviation.Split('-');
            if (parts.Length != 2)
                return BadRequest($"Invalid format for team: {item.NameAbbreviation}. Expected format: 'Team Name-ABC'.");

            string teamName = TeamNameHelper.Clean(parts[0]);
            var existingTeamByName = await uow.Teams.GetByNameAsync(teamName);
            if (existingTeamByName != null)
                return BadRequest($"A team with the name '{teamName}' already exists. No teams were created.");
            if (teams.Any(t => t.TeamName.Equals(teamName, StringComparison.OrdinalIgnoreCase)))
                return BadRequest($"Duplicate team name '{teamName}' in the request. No teams were created.");

            string abbreviation = TeamNameHelper.Clean(parts[1]);
            var existingTeamByAbbr = await uow.Teams.GetByAbbreviationAsync(abbreviation);
            if (existingTeamByAbbr != null)
                return BadRequest($"A team with the abbreviation '{abbreviation}' already exists. No teams were created.");
            if (teams.Any(t => t.Abbreviation.Equals(abbreviation, StringComparison.OrdinalIgnoreCase)))
                return BadRequest($"Duplicate abbreviation '{abbreviation}' in the request. No teams were created.");

            var newTeam = new Team
            {
                TeamName = teamName,
                Abbreviation = abbreviation
            };
            teams.Add(newTeam);
        }

        foreach (var team in teams)
        {
            await uow.Teams.CreateAsync(team);
        }

        try
        {
            if (await uow.CompleteAsync())
            {
                return Ok("Teams created successfully.");
            }
            return BadRequest("Failed to create teams.");
        }
        catch (DbUpdateException exception) when (IsUniqueConstraintViolation(exception))
        {
            return Conflict("A team with the same name or abbreviation already exists. No teams were created.");
        }
    }

    [HttpPatch("{teamId:int}")]
    public async Task<ActionResult> UpdateTeam(int teamId, UpdateTeamDto teamDto)
    {
        var team = await uow.Teams.GetByIdAsync(teamId);
        if (team is null) return NotFound("Team not found.");

        var existingTeamByName = await uow.Teams.GetByNameAsync(teamDto.TeamName ?? string.Empty);
        if (existingTeamByName != null && existingTeamByName.TeamID != teamId)
            return BadRequest("A team with the same name already exists.");
        team.TeamName = string.IsNullOrWhiteSpace(teamDto.TeamName) ? team.TeamName : teamDto.TeamName;

        var existingTeamByAbbr = await uow.Teams.GetByAbbreviationAsync(teamDto.Abbreviation ?? string.Empty);
        if (existingTeamByAbbr != null && existingTeamByAbbr.TeamID != teamId)
            return BadRequest("A team with the same abbreviation already exists.");
        team.Abbreviation = string.IsNullOrWhiteSpace(teamDto.Abbreviation) ? team.Abbreviation : teamDto.Abbreviation;

        try
        {
            if (await uow.CompleteAsync())
            {
                return Ok("Team updated successfully.");
            }
            return BadRequest("Team not updated. Please check the provided data.");
        }
        catch (DbUpdateException exception) when (IsUniqueConstraintViolation(exception))
        {
            return Conflict("A team with the same name or abbreviation already exists.");
        }

    }

    [HttpDelete("{teamId:int}")]
    public async Task<ActionResult> DeleteTeam(int teamId)
    {
        var team = await uow.Teams.GetByIdAsync(teamId);
        if (team == null)
        {
            return NotFound("Team not found.");
        }

        var statuses = TournamentStatus.Upcoming | TournamentStatus.Ongoing | TournamentStatus.Completed;

        var tournaments = await uow.Tournaments.GetByStatusAsync(statuses);
        if (tournaments.Any(t => t.TournamentTeams.Any(tt => tt.TeamID == teamId)))
            return BadRequest("Cannot delete team. It is associated with one or more ongoing tournaments.");

        await uow.Teams.DeleteAsync(team);

        if (await uow.CompleteAsync())
        {
            return Ok("Team deleted successfully.");
        }
        return BadRequest("Failed to delete team.");
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException exception)
    {
        return exception.GetBaseException() is SqlException { Number: 2601 or 2627 };
    }
}
