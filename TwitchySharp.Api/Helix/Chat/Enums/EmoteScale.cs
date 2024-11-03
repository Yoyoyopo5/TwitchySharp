using System;
using System.Text.Json.Serialization;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Defines the possible scales of an emote image.
/// </summary>

[JsonConverter(typeof(EmoteScaleJsonConverter))]
public enum EmoteScale
{
    /// <summary>
    /// 28px x 28px
    /// </summary>
    Small,
    /// <summary>
    /// 56px x 56px
    /// </summary>
    Medium,
    /// <summary>
    /// 112px x 112px
    /// </summary>
    Large
}

internal static class EmoteScaleExtensions
{
    /// <summary>
    /// 28px
    /// </summary>
    private const string SMALL = "1.0";
    /// <summary>
    /// 56px
    /// </summary>
    private const string MEDIUM = "2.0";
    /// <summary>
    /// 112px
    /// </summary>
    private const string LARGE = "3.0";
    /// <summary>
    /// Internal function to convert <see cref="EmoteScale"/> to string that Twitch expects.
    /// </summary>
    /// <param name="emoteScale"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string ToNumericalString(this EmoteScale emoteScale)
        => emoteScale switch
        {
            EmoteScale.Small => SMALL,
            EmoteScale.Medium => MEDIUM,
            EmoteScale.Large => LARGE,
            _ => throw new ArgumentException("Emote scale input was not a valid enum value.")
        };

    /// <summary>
    /// Internal function to convert a valid <see langword="string"/> to an EmoteScale.
    /// Throws <see cref="ArgumentException"/> if <paramref name="emoteScaleString"/> is not a a valid scale.
    /// </summary>
    /// <param name="emoteScaleString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static EmoteScale ToEmoteScale(this string emoteScaleString)
        => emoteScaleString switch
        {
            SMALL => EmoteScale.Small,
            MEDIUM => EmoteScale.Medium,
            LARGE => EmoteScale.Large,
            _ => throw new ArgumentException("Emote scale string did not convert to a valid enum value.")
        };
}