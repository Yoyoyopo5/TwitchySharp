using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains static definitions for possible emote types.
/// </summary>
/// <param name="Value">The string value of the emote type.</param>
[JsonConverter(typeof(ValueBackedEnumJsonConverter<EmoteType, string>))]
public record EmoteType(string Value) : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// No emote type was assigned to this emote.
    /// </summary>
    public static EmoteType None { get; } = new("none");
    /// <summary>
    /// A custom Bits tier emote.
    /// </summary>
    public static EmoteType Bitstier { get; } = new("bitstier");
    /// <summary>
    /// A custom follower emote.
    /// </summary>
    public static EmoteType Follower { get; } = new("follower");
    /// <summary>
    /// A custom subscriber emote.
    /// </summary>
    public static EmoteType Subscriptions { get; } = new("subscriptions");
    /// <summary>
    /// An emote granted by using channel points.
    /// </summary>
    public static EmoteType ChannelPoints { get; } = new("channel_points");
    /// <summary>
    /// An emote granted to the user through a special event.
    /// </summary>
    public static EmoteType Rewards { get; } = new("rewards");
    /// <summary>
    /// An emote granted for participation in a Hype Train.
    /// </summary>
    public static EmoteType HypeTrain { get; } = new("hype_train");
    /// <summary>
    /// An emote granted for linking an Amazon Prime account.
    /// </summary>
    public static EmoteType Prime { get; } = new("prime");
    /// <summary>
    /// An emote granted for having Twitch Turbo.
    /// </summary>
    public static EmoteType Turbo { get; } = new("turbo");
    /// <summary>
    /// Emoticons supported by Twitch.
    /// </summary>
    public static EmoteType Smilies { get; } = new("smilies");
    /// <summary>
    /// An emote accessible by everyone.
    /// </summary>
    public static EmoteType Globals { get; } = new("globals");
    /// <summary>
    /// Emotes related to Overwatch League 2019.
    /// </summary>
    public static EmoteType Owl2019 { get; } = new("owl_2019");
    /// <summary>
    /// Emotes granted by enabling two-factor authentication on an account.
    /// </summary>
    public static EmoteType TwoFactor { get; } = new("two_factor");
    /// <summary>
    /// Emotes that were granted for only a limited time.
    /// </summary>
    public static EmoteType LimitedTime { get; } = new("limited_time");
}
