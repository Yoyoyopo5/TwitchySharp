
namespace TwitchySharp.Api;

/// <summary>
/// Contains the authorization values to be set as HTTP headers on a Twitch API request.
/// </summary>
/// <param name="ClientId">
/// The client ID to set in the <c>Client-Id</c> header.
/// May be null for requests that only require a bearer token (e.g., some authorization endpoints).
/// </param>
/// <param name="BearerToken">
/// The access token to set in the <c>Authorization: Bearer</c> header.
/// May be null for requests that only require a client ID.
/// </param>
public readonly record struct TwitchAuthorizationHeaders(ClientId? ClientId, IAccessToken? BearerToken);
