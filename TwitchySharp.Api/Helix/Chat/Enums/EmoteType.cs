namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Describes the type of an emote.
/// </summary>
public enum EmoteType
{
    /// <summary>
    /// No emote type was assigned to this emote.
    /// </summary>
    None,
    /// <summary>
    /// A custom Bits tier emote.
    /// </summary>
    Bitstier,
    /// <summary>
    /// A custom follower emote.
    /// </summary>
    Follower,
    /// <summary>
    /// A custom subscriber emote.
    /// </summary>
    Subscriptions,
    /// <summary>
    /// An emote granted by using channel points.
    /// </summary>
    ChannelPoints,
    /// <summary>
    /// An emote granted to the user through a special event.
    /// </summary>
    Rewards,
    /// <summary>
    /// An emote granted for participation in a Hype Train.
    /// </summary>
    HypeTrain,
    /// <summary>
    /// An emote granted for linking an Amazon Prime account.
    /// </summary>
    Prime,
    /// <summary>
    /// An emote granted for having Twitch Turbo.
    /// </summary>
    Turbo,
    /// <summary>
    /// Emoticons supported by Twitch.
    /// </summary>
    Smilies,
    /// <summary>
    /// An emote accessible by everyone.
    /// </summary>
    Globals,
    /// <summary>
    /// Emotes related to Overwatch League 2019.
    /// </summary>
    Owl2019,
    /// <summary>
    /// Emotes granted by enabling two-factor authentication on an account.
    /// </summary>
    TwoFactor,
    /// <summary>
    /// Emotes that were granted for only a limited time.
    /// </summary>
    LimitedTime
}
