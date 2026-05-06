using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Charity;
/// <summary>
/// Gets information about the charity campaign that a broadcaster is running.
/// </summary>
/// <remarks>
/// For example, the campaign's fundraising goal and the current amount of donations.
/// <br/>
/// Requires a user access token with <see cref="Scope.ChannelReadCharity"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-charity-campaign">get charity campaign</see> for more information.
/// </remarks>
public record GetCharityCampaignRequest
    : TwitchHelixRequest<GetCharityCampaignResponse>
{
    protected override string Path => "/charity/campaigns";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadCharity)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster to get charity campaign data for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.ChannelReadCharity"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
}
