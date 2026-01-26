using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;

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
    : TwitchHelixRequest<GetConduitsResponse>
{
    protected override string Path => "/eventsub/conduits";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.Default;
    public override IEnumerable<Scope> ValidScopes => [];
}
