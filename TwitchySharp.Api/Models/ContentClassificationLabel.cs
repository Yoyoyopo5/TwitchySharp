using TwitchySharp.Shared.Enums;

namespace TwitchySharp.Api;

/// <summary>
/// A label that indicates whether a specific CCL is enabled on a channel.
/// </summary>
/// <param name="Id">The id of the Content Classification Labels that should be added/removed from the channel.</param>
/// <param name="IsEnabled">Boolean flag indicating whether the label should be enabled or disabled for the channel.</param>
public readonly record struct ContentClassificationLabel(ContentClassificationLabelId Id, bool IsEnabled)
{
    /// <summary>
    /// The id of the Content Classification Labels that should be added/removed from the channel.
    /// </summary>
    public ContentClassificationLabelId Id { get; } = Id;
    /// <summary>
    /// Boolean flag indicating whether the label should be enabled or disabled for the channel.
    /// </summary>
    public bool IsEnabled { get; } = IsEnabled;
}
