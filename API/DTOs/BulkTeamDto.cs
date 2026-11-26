using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class BulkTeamDto
{
    [Required]
    public required List<BulkTeamItemDto> Teams { get; set; }
}

public class BulkTeamItemDto
{
    [Required, RegularExpression(@"\A(?=[A-Za-z ]*[A-Za-z])[A-Za-z ]{1,40}-[A-Za-z]{3}\z",
    ErrorMessage = "Each item must have a team name (letters and spaces) and a 3-letter abbreviation, separated by a dash.")]
    public required string NameAbbreviation { get; set; }
}
