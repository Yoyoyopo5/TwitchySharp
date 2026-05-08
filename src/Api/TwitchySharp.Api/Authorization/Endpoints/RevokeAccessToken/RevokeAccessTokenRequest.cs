using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Revokes a valid app or user access token so that it is no longer valid.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/authentication/revoke-tokens/">Revoke Tokens</see> for more information.
/// </remarks>
public record RevokeAccessTokenRequest
    : TwitchAuthorizationRequest<RevokeAccessTokenResponse>
{
    protected override string Path => "/revoke";
    public override HttpMethod Method => HttpMethod.Post;
    public override HttpContent? Content
        => new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            { "client_id", ClientId },
            { "token", AccessToken.Value }
        });

    /// <summary>
    /// The client id of the application that the <see cref="IAccessToken"/> was created under.
    /// </summary>
    public required ClientId ClientId { get; init; }
    /// <summary>
    /// The access token to revoke.
    /// </summary>
    public required IAccessToken AccessToken { get; init; }

    protected override ValueTask<RevokeAccessTokenResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new RevokeAccessTokenResponse());
}
