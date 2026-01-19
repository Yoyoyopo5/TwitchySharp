using System;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains information about a specific extension secret.
/// </summary>
public record ExtensionSecret
{
    /// <summary>
    /// The raw secret that you use with JWT encoding.
    /// </summary>
    public required string Content { get; init; }
    /// <summary>
    /// The date and time that you may begin using this secret to sign a JWT.
    /// </summary>
    public required DateTimeOffset ActiveAt { get; init; }
    /// <summary>
    /// The date and time when you must stop using this secret to decode a JWT.
    /// </summary>
    public required DateTimeOffset ExpiresAt { get; init; }
}
