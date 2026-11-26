using System.Text.RegularExpressions;

namespace API.Helpers;

public static partial class TeamNameHelper
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex Whitespace();

    public static string Clean(string? name) =>
        string.IsNullOrWhiteSpace(name) ? string.Empty : Whitespace().Replace(name.Trim(), " ");

    public static string Key(string? name) => Clean(name).ToUpperInvariant();
}
