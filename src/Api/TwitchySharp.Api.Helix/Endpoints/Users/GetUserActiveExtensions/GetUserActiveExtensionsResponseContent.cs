namespace TwitchySharp.Api.Helix.Users;
/// <inheritdoc cref="UserActiveExtensions"/>
public record GetUserActiveExtensionsResponseContent
{
    /// <summary>
    /// The channel's extension slots.
    /// </summary>
    public required UserActiveExtensions Data { get; init; }
}
