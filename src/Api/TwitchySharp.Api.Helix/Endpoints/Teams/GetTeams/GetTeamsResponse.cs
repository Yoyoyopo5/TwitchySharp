namespace TwitchySharp.Api.Helix.Teams;
/// <inheritdoc cref="TwitchTeam"/>
public record GetTeamsResponse
{
    /// <summary>
    /// A list that contains a single team.
    /// </summary>
    public required TwitchTeam[] Data { get; init; }
}
