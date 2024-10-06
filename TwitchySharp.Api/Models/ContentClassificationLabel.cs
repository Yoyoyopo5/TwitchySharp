using System.Text.Json.Serialization;
using TwitchySharp.Api.Helix.Channels;

namespace TwitchySharp.Api.Models;

/// <summary>
/// A label that indicates whether a specific CCL is enabled on a channel.
/// </summary>
/// <param name="Id">The id of the Content Classification Labels that should be added/removed from the channel.</param>
/// <param name="IsEnabled">Boolean flag indicating whether the label should be enabled or disabled for the channel.</param>
public readonly record struct ContentClassificationLabel(ContentClassificationLabelType Id, bool IsEnabled)
{
    /// <summary>
    /// The id of the Content Classification Labels that should be added/removed from the channel.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ContentClassificationLabelType Id { get; } = Id;
    /// <summary>
    /// Boolean flag indicating whether the label should be enabled or disabled for the channel.
    /// </summary>
    public bool IsEnabled { get; } = IsEnabled;
}

/// <summary>
/// Types of Twitch CCLs.
/// </summary>
public enum ContentClassificationLabelType
{
    DrugsIntoxication,
    SexualThemes,
    ViolentGraphic,
    Gambling,
    ProfanityVulgarity
}
