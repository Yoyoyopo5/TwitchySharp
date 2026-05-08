namespace TwitchySharp.Api.Authorization;
/// <summary>
/// Used to get a device code from Twitch which can be used to get a user access token for a specific device.
/// </summary>
/// <remarks>
/// Uses the <see href="https://dev.twitch.tv/docs/authentication/getting-tokens-oauth/#device-code-grant-flow">device code grant flow</see>.
/// </remarks>
public record DeviceCodeRequest
    : TwitchAuthorizationRequest<DeviceCodeResponse>
{
    protected override string Path => "/device";
    public override HttpMethod Method => HttpMethod.Post;
    public override HttpContent? Content
        => new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "client_id", ClientId },
            { "scopes", Scopes.FormatScopes() }
        });

    /// <summary>
    /// The client id of the application.
    /// </summary>
    public required ClientId ClientId { get; init; }
    /// <summary>
    /// The <see href="https://dev.twitch.tv/docs/authentication/scopes/">authorization scopes</see> to request.
    /// </summary>
    public required IEnumerable<Scope> Scopes { get; init; }
}
