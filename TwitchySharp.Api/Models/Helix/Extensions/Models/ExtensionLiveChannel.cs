namespace TwitchySharp.Api.Models.Helix.Extensions.Models;

/// <summary>
/// Contains information about a broadcaster that has installed or activated a specific extension.
/// </summary>
public record ExtensionLiveChannel
{
    /// <summary>
    /// The user id of the broadcaster that is using the extension.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The broadcaster's display name.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The name of the category being streamed.
    /// </summary>
    public required string GameName { get; init; }
    /// <summary>
    /// The game id of the category being streamed.
    /// </summary>
    public required string GameId { get; init; }
    /// <summary>
    /// The title of the broadcaster's livestream. This may be an empty string.
    /// </summary>
    public required string Title { get; init; }
}
