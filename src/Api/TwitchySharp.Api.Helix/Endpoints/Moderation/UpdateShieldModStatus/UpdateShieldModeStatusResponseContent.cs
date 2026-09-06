namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains information about a channel's Shield Mode status.
/// </summary>
public record UpdateShieldModeStatusResponseContent
{
    /// <summary>
    /// A list containing a single object of the channel's Shield Mode status.
    /// </summary>
    public required ShieldModeStatus[] Data { get; init; }
}
