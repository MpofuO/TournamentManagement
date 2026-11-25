using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class TeamDto
{
    [Required, StringLength(40, MinimumLength = 2, ErrorMessage = "Team name must be between 2 and 40 characters long.")]
    public required string TeamName {get; set;}

    [Required, StringLength(3, MinimumLength = 3, ErrorMessage = "Abbreviation must be exactly 3 characters long.")]
    public required string Abbreviation {get; set;}
}

public class UpdateTeamDto
{
    [StringLength(40, MinimumLength = 2, ErrorMessage = "Team name must be between 2 and 40 characters long.")]
    public string? TeamName { get; set; }

    [StringLength(3, MinimumLength = 3, ErrorMessage = "Abbreviation must be exactly 3 characters long.")]
    public string? Abbreviation { get; set; }
}
