namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of chat settings.
/// </summary>
public record GetChatSettingsResponse
{
    /// <summary>
    /// The list of chat settings. 
    /// The list contains a single object with all the settings.
    /// </summary>
    public required ChatSettings[] Data { get; init; }
}
