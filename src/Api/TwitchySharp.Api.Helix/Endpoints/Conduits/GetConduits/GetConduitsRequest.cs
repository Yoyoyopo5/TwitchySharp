namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Gets the conduits for a specific client id.
/// </summary>
/// <remarks>
/// Requires an app access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-conduits">Get Conduits</see> for more information.
/// </remarks>
public record GetConduitsRequest
    : TwitchHelixRequest<GetConduitsResponseContent>,
    IAuthenticatedTwitchRequest<TwitchRequestAuthenticationContext<TwitchIdentity.Client>>
{
    protected override string Path => "/eventsub/conduits";
    public override HttpMethod Method => HttpMethod.Get;
    public TwitchRequestAuthenticationContext<TwitchIdentity.Client> AuthenticationContext { get; init; }
        = TwitchRequestAuthenticationContext.Default;
}
