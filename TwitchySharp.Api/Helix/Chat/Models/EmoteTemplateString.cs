using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Holds template URL information for an emote's CDN image link.
/// Use <see cref="CreateEmoteImageUrl(string, TwitchySharp.Api.Helix.Chat.EmoteFormat, TwitchySharp.Api.Helix.Chat.EmoteTheme, TwitchySharp.Api.Helix.Chat.EmoteScale)"/>
/// to create a URL pointing to a specific emote's image data. 
/// </summary>
public readonly record struct EmoteImageTemplateString
{
    /// <summary>
    /// The template string for the emote. 
    /// This is returned from the Twitch API in some responses (e.g. <see cref="GetChannelEmotesResponse"/>).
    /// </summary>
    public required string TemplateString { get; init; } // Considered making internal but whatever. Use with care.
    /// <summary>
    /// Creates a CDN request URL as outlined in <see href="https://dev.twitch.tv/docs/chat/send-receive-messages/#cdn-template">CDN template</see>.
    /// Use the returned URL to make a request for an emote's image data.
    /// </summary>
    /// <param name="emoteId">The id of the emote.</param>
    /// <param name="format">The format to get the image in.</param>
    /// <param name="theme">The background theme to get the image in.</param>
    /// <param name="scale">The scale to get the emote in.</param>
    /// <returns></returns>
    public string CreateEmoteImageUrl(string emoteId, EmoteFormat format, EmoteTheme theme, EmoteScale scale)
    {
        return TemplateString
            .Replace("{{id}}", emoteId)
            .Replace("{{format}}", format.ToString().ToLower())
            .Replace("{{theme_mode}}", theme.ToString().ToLower())
            .Replace("{{scale}}", scale.ToNumericalString());
    }
    /// <summary>
    /// </summary>
    /// <returns>The <see cref="TemplateString"/></returns>
    public override string ToString()
        => TemplateString;
}
