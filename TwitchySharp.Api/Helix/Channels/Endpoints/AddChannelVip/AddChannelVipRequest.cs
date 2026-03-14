using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.ChannelPoints;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Adds the specified user as a VIP in the broadcaster's channel.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> A broadcaster may add a maximum of 10 VIPs within a 10-second window.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageVips"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#add-channel-vip">Add Channel VIP</see> for more information.
/// </remarks>
public record AddChannelVipRequest
    : TwitchHelixRequest<AddChannelVipResponse>
{
    protected override string Path => "/channels/vips";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(BroadcasterId);
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.ChannelManageVips);
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("user_id", UserId);

    /// <summary>
    /// The user id of the broadcaster (channel) to add a VIP for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.ChannelManageVips"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The id of the user to give VIP status to.
    /// </summary>
    public required UserId UserId { get; init; }

    protected override ValueTask<AddChannelVipResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new AddChannelVipResponse());
}
