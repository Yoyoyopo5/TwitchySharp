namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Used to get a user access token using the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">device code grant flow</see>.
/// </summary>
public record DeviceCodeTokenRequest
    : TwitchAuthorizationRequest<DeviceCodeTokenResponse>
{
    protected override string Path => "/token";
    public override HttpMethod Method => HttpMethod.Post;
    public override HttpContent? Content
        => new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", ClientId },
            { "scope", Scopes.FormatScopes() },
            { "device_code", DeviceCode },
            { "grant_type", "urn:ietf:params:oauth:grant-type:device_code" }
        });

    /// <summary>
    /// The client id of the application making the request.
    /// </summary>
    public required ClientId ClientId { get; init; }
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> to request.
    /// </summary>
    public required IEnumerable<Scope> Scopes { get; init; }
    /// <summary>
    /// The device code obtained from a <see cref="DeviceCodeRequest"/>.
    /// </summary>
    public required DeviceCode DeviceCode { get; init; }
}
