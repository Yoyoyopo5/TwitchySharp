namespace TwitchySharp.Api.Helix.Users;
/// <inheritdoc cref="UserActiveExtensions"/>
public record GetUserActiveExtensionsResponse
{
    /// <summary>
    /// The active extensions that the broadcaster has installed.
    /// </summary>
    public required UserActiveExtensions Data { get; init; }
}
