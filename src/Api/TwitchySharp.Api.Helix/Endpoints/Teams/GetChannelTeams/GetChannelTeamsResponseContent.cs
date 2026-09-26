namespace TwitchySharp.Api.Helix.Teams;
/// <summary>
/// Contains a list of teams a broadcaster belongs to.
/// </summary>
public record GetChannelTeamsResponseContent
{
    /// <summary>
    /// The list of teams the broadcaster belongs to.
    /// </summary>
    public required BroadcasterTeam[] Data { get; init; }
}
