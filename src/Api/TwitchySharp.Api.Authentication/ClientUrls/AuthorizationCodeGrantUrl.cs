using System.Collections.Immutable;

namespace TwitchySharp.Api.Authentication;
/// <summary>
/// Encodes a url used to authenticate a user via the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#authorization-code-grant-flow">authorization code grant flow</see>.
/// </summary>
/// <remarks>
/// This is the reccomended way to authorize users for apps that use a server.
/// A <c>code</c> query parameter will be sent to the <see cref="AuthorizationUrl.RedirectUri"/> when the user authorizes via this URI that can be used in an <see cref="AuthorizationCodeRequest"/> to obtain a <see cref="UserAccessToken"/> for that user.
/// </remarks>
public record AuthorizationCodeGrantUrl
    : AuthorizationUrl
{
    protected override ImmutableHashSet<TwitchAuthorizationResponseType> ResponseTypes { get; init; } = [TwitchAuthorizationResponseType.Code];
}
