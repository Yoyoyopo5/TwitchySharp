namespace TwitchySharp.Api.Helix.Chat;

public record GetEmoteSetsResponse
{
    /// <summary>
    /// The list of emotes found in the specified emote sets.
    /// The list is empty if none of the IDs were found. 
    /// The list is in the same order as the set IDs specified in the request. 
    /// Each set contains one or more emoticons.
    /// </summary>
    public required EmoteSetEmote[] Data { get; init; }
    /// <summary>
    /// A templated URL for getting an emote image.
    /// </summary>
    public required EmoteImageTemplateString Template { get; init; }
}