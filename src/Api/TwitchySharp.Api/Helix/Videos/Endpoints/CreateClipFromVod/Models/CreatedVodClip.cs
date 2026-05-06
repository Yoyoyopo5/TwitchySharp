using System;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Videos;

/// <summary>
/// A clip created from a Twitch VOD.
/// </summary>
public record CreatedVodClip
{
    /// <summary>
    /// The id of the created clip.
    /// </summary>
    public required ClipId Id { get; init; }
    /// <summary>
    /// A url you can use to edit the clip’s title, feature the clip, create a portrait version of the clip, download the clip media, and share the clip directly to third-party platforms.
    /// </summary>
    public required Uri EditUrl { get; init; }
}
