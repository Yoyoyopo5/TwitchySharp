namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of a channel's moderators.
/// </summary>
public record GetModeratorsResponse
    : IPageableResponse
{
    /// <summary>
    /// The list of moderators for the specified channel.
    /// </summary>
    public required Moderator[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
