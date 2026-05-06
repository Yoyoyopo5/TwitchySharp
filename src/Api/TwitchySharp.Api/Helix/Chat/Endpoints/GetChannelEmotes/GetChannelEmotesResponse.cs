namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of custom emotes.
/// </summary>
public record GetChannelEmotesResponse
{
    /// <summary>
    /// The list of emotes that the specified broadcaster created. 
    /// If the broadcaster hasn't created custom emotes, the list is empty.
    /// </summary>
    public required ChannelEmote[] Data { get; init; }
    /// <summary>
    /// A templated URL for getting an emote image.
    /// </summary>
    public required EmoteImageTemplateString Template { get; init; }
}