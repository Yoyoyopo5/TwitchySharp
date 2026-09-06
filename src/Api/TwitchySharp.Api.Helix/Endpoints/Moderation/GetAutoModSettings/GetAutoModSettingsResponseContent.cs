namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of AutoMod settings.
/// </summary>
public record GetAutoModSettingsResponseContent
{
    /// <summary>
    /// Contains a single entry of a channel's current AutoMod settings.
    /// </summary>
    public required AutoModSettings[] Data { get; init; }
}
