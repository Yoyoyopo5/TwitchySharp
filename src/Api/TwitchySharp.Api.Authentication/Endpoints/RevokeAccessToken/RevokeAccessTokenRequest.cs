namespace TwitchySharp.Api.Authentication;
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

    public override Func<Stream, CancellationToken, ValueTask<RevokeAccessTokenResponse>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new RevokeAccessTokenResponse());
}
