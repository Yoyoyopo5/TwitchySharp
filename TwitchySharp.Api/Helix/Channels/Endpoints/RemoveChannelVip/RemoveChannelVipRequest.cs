using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Removes the specified user as a VIP in the broadcaster's channel.
/// </summary>
/// <remarks>
/// Note that this endpoint can be used to remove VIP status from a user on their behalf. In this case, the access token can be created by the user instead of the broadcaster.
/// <b>Rate Limits:</b> A broadcaster may remove a maximum of 10 VIPs within a 10-second window.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageVips"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-channel-vip">Remove Channel VIP</see> for more information.
/// </remarks>
public record RemoveChannelVipRequest
    : TwitchHelixRequest<RemoveChannelVipResponse>
{
    protected override string Path => "/channels/vips";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelManageVips);
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("user_id", UserId);

    /// <summary>
    /// The user id of the broadcaster (channel) to remove a VIP for.
    /// </summary>
    /// <remarks>
    /// If removing a user's VIP status on behalf of the broadcaster, the broadcaster must have created the access token used in the request.
    /// Requires <see cref="Scope.ChannelManageVips"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The id of the user to revoke VIP status for.
    /// </summary>
    /// <remarks>
    /// If removing this user's VIP status on behalf of the user themselves, this user can have created the access token used in the request.
    /// </remarks>
    public required UserId UserId { get; init; }
}
