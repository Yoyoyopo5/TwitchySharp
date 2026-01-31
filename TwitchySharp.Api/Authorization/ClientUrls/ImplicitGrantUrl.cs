using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Authorization.ClientUrls;
/// <summary>
/// Encodes a url used to authenticate a user via the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#implicit-grant-flow">implicit grant flow</see>.
/// </summary>
/// <remarks>
/// <para>
/// This is the recommended way to authorize users for client-side (e.g. JavaScript) applications because it does not require a client secret to obtain an access token.
/// If you can securely store a client secret and make server-to-server requests, you should use the <see cref="AuthorizationCodeGrantUrl"/> instead.
/// </para>
/// <para>
/// Returns a <see cref="UserAccessToken"/> directly in the fragment of the <see cref="AuthorizationUrl.RedirectUri"/> after the user authorizes via this URI.
/// </para>
/// </remarks>
public record ImplicitGrantUrl : AuthorizationUrl
{
    /// <summary>
    /// These response types will be included in the redirect after authorization.
    /// </summary>
    /// <remarks>
    /// See <see cref="TwitchAuthorizationResponseType.Token"/> and <see cref="TwitchAuthorizationResponseType.IdToken"/>.
    /// </remarks>
    public required ImmutableHashSet<TwitchAuthorizationResponseType> IncludeResponseTypes { get => ResponseTypes; init => ResponseTypes = value; }
    protected override ImmutableHashSet<TwitchAuthorizationResponseType> ResponseTypes { get; init; } = [];
}
