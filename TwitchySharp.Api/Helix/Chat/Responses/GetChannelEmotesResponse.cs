using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
    /// A templated URL. 
    /// Use the values from the <see cref="ChannelEmote.Id"/>, <see cref="ChannelEmote.Format"/>, <see cref="ChannelEmote.Scale"/>, and <see cref="ChannelEmote.ThemeMode"/> properties to replace the like-named placeholder strings in the templated URL to create a CDN (content delivery network) URL that you use to fetch the emote. 
    /// For information about what the template looks like and how to use it to fetch emotes, see <see href="https://dev.twitch.tv/docs/chat/send-receive-messages/#cdn-template">Emote CDN URL format</see>. 
    /// You should use this template instead of using the URLs in the images object.
    /// </summary>
    public required EmoteImageTemplateString Template { get; init; }
}

public record ChannelEmote
{
    /// <summary>
    /// An ID that identifies this emote.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The name of the emote. 
    /// This is the name that viewers type in the chat window to get the emote to appear.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// The image URLs for the emote. 
    /// These image URLs always provide a static, non-animated emote image with a light background.
    /// <b>NOTE:</b> You should use the <see cref="EmoteImageTemplateString"/> in the Template property to fetch the image instead of using these URLs.
    /// </summary>
    public required EmoteImage Images { get; init; }
    /// <summary>
    /// The subscriber tier at which the emote is unlocked. 
    /// This field contains the tier information only if <see cref="EmoteType"/> is set to <see cref="EmoteType.Subscriptions"/>, otherwise, it's an empty string.
    /// </summary>
    public required string Tier { get; init; }
    /// <summary>
    /// The type of emote.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required EmoteType EmoteType { get; init; }
    /// <summary>
    /// An ID that identifies the emote set that the emote belongs to.
    /// </summary>
    public required string EmoteSetId { get; init; }
    /// <summary>
    /// The formats that the emote is available in. 
    /// For example, if the emote is available only as a static PNG, the array contains only <see cref="EmoteFormat.Static"/>. 
    /// But if the emote is available as a static PNG and an animated GIF, the array contains <see cref="EmoteFormat.Static"/> and <see cref="EmoteFormat.Animated"/>.
    /// </summary>
    public required EmoteFormat[] Format { get; init; } // It would be cool to write a converter that can convert an array of strings into a Flags enum instead of an enum array.
    /// <summary>
    /// The sizes that the emote is available in. 
    /// For example, if the emote is available in small and medium sizes, the array contains <see cref="EmoteScale"/>
    /// </summary>
    public required EmoteScale[] Scale { get; init; }
    /// <summary>
    /// The background themes that the emote is available in.
    /// </summary>
    public required EmoteTheme[] ThemeMode { get; init; }
}
