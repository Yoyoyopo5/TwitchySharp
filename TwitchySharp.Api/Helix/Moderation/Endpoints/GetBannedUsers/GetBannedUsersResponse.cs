namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of a channel's banned or timed-out users.
/// </summary>
public record GetBannedUsersResponse
    : IPageableResponse
{
    /// <summary>
    /// A list of the channel's banned and timed-out users.
    /// </summary>
    public required BannedUser[] Data { get; init; }
    /// <summary>
    /// <inheritdoc cref="Models.Pagination"/>
    /// </summary>
    public required Pagination Pagination { get; init; }
}