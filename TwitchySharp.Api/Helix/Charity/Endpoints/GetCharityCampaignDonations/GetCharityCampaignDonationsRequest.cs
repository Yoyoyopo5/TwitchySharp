using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Charity;
/// <summary>
/// Gets the list of donations that users have made to the broadcaster's active charity campaign.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadCharity"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-charity-campaign-donations">Get Charity Campaign Donations</see> for more information.
/// </remarks>
public record GetCharityCampaignDonationsRequest
    : TwitchHelixRequest<GetCharityCampaignDonationsResponse>, IPageableRequest
{
    protected override string Path => "/charity/donations";
    public override HttpMethod Method => HttpMethod.Get;
    public override TwitchRequestAuthorizationContext AuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadCharity)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the broadcaster that you want to get charity donations for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.ChannelReadCharity"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }
}
