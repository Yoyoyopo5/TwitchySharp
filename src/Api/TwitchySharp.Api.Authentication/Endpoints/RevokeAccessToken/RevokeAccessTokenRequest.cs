namespace TwitchySharp.Api.Authentication;
/// <summary>
/// Revokes a valid app or user access token so that it is no longer valid.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/authentication/revoke-tokens/">Revoke Tokens</see> for more information.
/// </remarks>
public record RevokeAccessTokenRequest
    : TwitchAuthenticationRequest<RevokeAccessTokenResponseContent>
{
    /// <inheritdoc/>
    protected override string Path => "/revoke";
    /// <inheritdoc/>
    public override HttpMethod Method => HttpMethod.Post;
    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public override ValueTask<TwitchResponse<RevokeAccessTokenResponseContent>> CreateResponse(HttpResponseMessage response, CancellationToken ct)
    {
        using HttpResponseMessage r = response;
        return ValueTask.FromResult(new TwitchResponse<RevokeAccessTokenResponseContent>(new RevokeAccessTokenResponseContent()));
    }
}
