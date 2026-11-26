using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class TeamDto
{
    public int? TeamId { get; set; }

    [Required, RegularExpression(@"\A(?=[A-Za-z ]*[A-Za-z])[A-Za-z ]+\z", ErrorMessage = "Team name can only contain letters and spaces.")]
    [StringLength(40, MinimumLength = 1, ErrorMessage = "Team name must be between 1 and 40 characters long.")]
    public required string TeamName { get; set; }

    [RegularExpression(@"\A[A-Za-z]{3}\z", ErrorMessage = "Abbreviation must be exactly 3 letters.")]
    public required string Abbreviation { get; set; }
}

public class UpdateTeamDto
{
    [RegularExpression(@"\A(?=[A-Za-z ]*[A-Za-z])[A-Za-z ]+\z", ErrorMessage = "Team name can only contain letters and spaces.")]
    [StringLength(40, MinimumLength = 1, ErrorMessage = "Team name must be between 1 and 40 characters long.")]
    public string? TeamName { get; set; }

    [RegularExpression(@"\A[A-Za-z]{3}\z", ErrorMessage = "Abbreviation must be exactly 3 letters.")]
    public string? Abbreviation { get; set; }
}
