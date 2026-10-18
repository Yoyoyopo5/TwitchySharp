namespace TwitchySharp.EventSub.Notifications;

/// <summary>
/// Contains information about a specific chat message GIF.
/// </summary>
public record ChannelChatMessageGif
{
    /// <summary>
    /// The id of the chat GIF.
    /// </summary>
    public required GifId Id { get; init; }
    /// <summary>
    /// A url pointing to the GIF asset.
    /// </summary>
    /// <remarks>
    /// Applications rendering the GIF must use the full URL provided; it must not be modified.
    /// </remarks>
    public required GifUrl Url { get; init; }
}

