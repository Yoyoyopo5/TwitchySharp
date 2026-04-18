using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains static values for possible Extension PubSub message targets.
/// </summary>
/// <param name="Value">The string value of the message target.</param>
[Wrapper<string>]
public readonly partial record struct ExtensionPubSubMessageTarget(string Value)
{
    /// <summary>
    /// Sends a message to a specific channel's chatroom.
    /// </summary>
    public static ExtensionPubSubMessageTarget Broadcast { get; } = new("broadcast");
    /// <summary>
    /// Sends a message to all channels on which the extension is active.
    /// </summary>
    public static ExtensionPubSubMessageTarget Global { get; } = new("global");
    /// <summary>
    /// Sends a message to a specific user as a whisper.
    /// </summary>
    /// <param name="userId">The user id of the user to send the message to.</param>
    /// <returns></returns>
    public static ExtensionPubSubMessageTarget Whisper(string userId) => new($"whisper-{userId}");
}
