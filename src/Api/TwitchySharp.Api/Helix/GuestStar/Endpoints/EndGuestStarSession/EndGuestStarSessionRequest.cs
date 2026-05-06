using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Ends a Guest Star session on behalf of the broadcaster.
/// </summary>
/// <remarks>
/// Performs the same action as if the host clicked the "End Call" button in the Guest Star UI.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#end-guest-star-session">End Guest Star Session</see> for more information.
/// </remarks>
public record EndGuestStarSessionRequest
    : TwitchHelixRequest<EndGuestStarSessionResponse>
{
    protected override string Path => "/guest_star/session";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageGuestStar)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("session_id", SessionId);

    /// <summary>
    /// The user id of the broadcaster to end a Guest Star session for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The id of the Guest Star session to end.
    /// </summary>
    public required GuestStarSessionId SessionId { get; init; }
}
