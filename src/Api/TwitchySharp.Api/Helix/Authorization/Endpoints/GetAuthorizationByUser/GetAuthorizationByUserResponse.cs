namespace TwitchySharp.Api.Helix.Authorization;
/// <summary>
/// Contains an array of user authorization data.
/// </summary>
public record GetAuthorizationByUserResponse
{
    /// <summary>
    /// An array of user authorization data.
    /// </summary>
    public required UserAuthorization[] Data { get; init; }
}
