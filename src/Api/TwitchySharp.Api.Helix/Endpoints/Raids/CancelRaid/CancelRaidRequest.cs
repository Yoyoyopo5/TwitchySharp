using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Raids;
/// <summary>
/// Cancel a pending raid.
/// </summary>
/// <remarks>
/// You can cancel a raid at any point up until the broadcaster clicks Raid Now in the Twitch UX or the 90-second countdown expires.
/// <br/>
/// <b>Rate Limits:</b> You may cancel up to 10 raids within a 10-minute window.
/// <para>
/// Requires a user access token that includes <see cref="Scope.ChannelManageRaids"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#cancel-a-raid">Cancel A Raid</see> for more information.
/// </remarks>
public record CancelRaidRequest
    : TwitchHelixRequest<CancelRaidResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/raids";
    public override HttpMethod Method => HttpMethod.Delete;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageRaids)
    };
    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster (channel) to cancel a pending raid for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<CancelRaidResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new CancelRaidResponseContent());
}
