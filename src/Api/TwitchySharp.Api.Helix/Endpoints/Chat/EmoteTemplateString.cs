using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Holds template URL information for an emote's CDN image link.
/// Use <see cref="CreateEmoteImageUrl(string, EmoteFormat, EmoteTheme, EmoteScale)"/>
/// to create a URL pointing to a specific emote's image data. 
/// </summary>
/// <param name="Value">
/// The template string for the emote. 
/// This is returned from the Twitch API in some responses (e.g. <see cref="GetChannelEmotesResponse"/>).
/// </param>
[Wrapper<string>]
public readonly partial record struct EmoteImageTemplateString(string Value)
{
    /// <summary>
    /// Creates a CDN request URL as outlined in <see href="https://dev.twitch.tv/docs/chat/send-receive-messages/#cdn-template">CDN template</see>.
    /// Use the returned URL to make a request for an emote's image data.
    /// </summary>
    /// <param name="emoteId">The id of the emote.</param>
    /// <param name="format">The format to get the image in.</param>
    /// <param name="theme">The background theme to get the image in.</param>
    /// <param name="scale">The scale to get the emote in.</param>
    /// <returns></returns>
    public ImageUrl CreateEmoteImageUrl(EmoteId emoteId, EmoteFormat format, EmoteTheme theme, EmoteScale scale)
        => new(Value
            .Replace("{{id}}", emoteId.Value)
            .Replace("{{format}}", format.Value)
            .Replace("{{theme_mode}}", theme.Value)
            .Replace("{{scale}}", scale.Value));
}
