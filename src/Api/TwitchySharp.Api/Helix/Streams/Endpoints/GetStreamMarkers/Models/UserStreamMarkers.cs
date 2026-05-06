using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Streams;

/// <summary>
/// Contains a list of stream markers made by a specific user on a specific video.
/// </summary>
public record UserStreamMarkers
{
    /// <summary>
    /// The id of the user that created the markers.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The display name of the user that created the markers.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The login (username) of the user that created the markers.
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The marked video.
    /// </summary>
    public required MarkedVideo[] Videos { get; init; }
}
