using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets a list of the broadcaster's VIPs.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadVips"/> or <see cref="Scope.ChannelManageVips"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-vips">Get VIPs</see> for more information.
/// </remarks>
public record GetVipsRequest
    : TwitchHelixRequest<GetVipsResponse>, IForwardPageableRequest
{
    protected override string Path => "/channels/vips";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelReadVips, Scope.ChannelManageVips)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("user_id", UserIds?.Select(x => x.ToString()))
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the broadcaster (channel) to get VIPs for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.ChannelReadVips"/> or <see cref="Scope.ChannelManageVips"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// Filter the list by specific users.
    /// </summary>
    /// <remarks>
    /// The maximum number of ids that you may specify is 100.
    /// Ignores the ids of users that aren't VIPs on the broadcaster's channel.
    /// </remarks>
    public IEnumerable<UserId>? UserIds { get; init; }

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
