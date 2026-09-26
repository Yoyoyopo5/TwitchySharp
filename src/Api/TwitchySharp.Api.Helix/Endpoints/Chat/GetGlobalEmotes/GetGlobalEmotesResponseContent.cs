namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of global emotes.
/// </summary>
public record GetGlobalEmotesResponseContent
{
    /// <summary>
    /// The list of global emotes.
    /// </summary>
    public required GlobalEmote[] Data { get; init; }
    /// <summary>
    /// A templated URL for getting an emote image.
    /// </summary>
    public required EmoteImageTemplateString Template { get; init; }
}
