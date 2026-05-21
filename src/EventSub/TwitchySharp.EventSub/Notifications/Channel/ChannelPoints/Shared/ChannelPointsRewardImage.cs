namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains URLs pointing to a specific channel point reward's image.
/// </summary>
public record ChannelPointsRewardImage
{
    /// <summary>
    /// URL for the image at 1x size (28x28).
    /// </summary>
    public required ImageUrl Url1x { get; init; }
    /// <summary>
    /// URL for the image at 2x size (56x56).
    /// </summary>
    public required ImageUrl Url2x { get; init; }
    /// <summary>
    /// URL for the image at 4x size (112x112).
    /// </summary>
    public required ImageUrl Url4x { get; init; }
}
