namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains an array containing the newly added suspicious user.
/// </summary>
public record AddSuspiciousStatusToChatUserResponse
{
    /// <summary>
    /// An array containing the single added suspicious user.
    /// </summary>
    public required SuspiciousUser[] Data { get; init; }
}
