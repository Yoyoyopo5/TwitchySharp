using System.Collections.Immutable;

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
    : TwitchHelixRequest<AddChannelVipResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/channels/vips";
    public override HttpMethod Method => HttpMethod.Post;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageVips)
    };
    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
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

    public override Func<Stream, CancellationToken, ValueTask<AddChannelVipResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new AddChannelVipResponseContent());
}
