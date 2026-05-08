using System;

namespace TwitchySharp.Api.Helix.Channels;

/// <summary>
/// Contains information about a specific editor on a channel.
/// </summary>
public record ChannelEditor
{
    /// <summary>
    /// The user ID of the channel editor.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The editor's Twitch display name.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The date and time when the user became one of the broadcaster’s editors.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
}
