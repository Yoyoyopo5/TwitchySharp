using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Raids;
/// <summary>
/// Raid another channel by sending the broadcaster's viewers to the targeted channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageRaids"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#start-a-raid">Start A Raid</see> for more information.
/// </remarks>
public record StartRaidRequest
    : TwitchHelixRequest<StartRaidResponse>
{
    protected override string Path => "/raids";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(FromBroadcasterId);
    public override IEnumerable<Scope> ValidScopes => [Scope.ChannelManageRaids];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("from_broadcaster_id", FromBroadcasterId)
            .Add("to_broadcaster_id", ToBroadcasterId);

    /// <summary>
    /// The user id of the broadcaster (channel) that is sending the raid.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId FromBroadcasterId { get; set; }

    /// <summary>
    /// The user id of the broadcaster (channel) to send the raid to.
    /// </summary>
    public required UserId ToBroadcasterId { get; set; }
}
