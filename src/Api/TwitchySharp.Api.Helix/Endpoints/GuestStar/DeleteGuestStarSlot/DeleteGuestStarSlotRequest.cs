using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// <b>BETA</b> Allows a caller to remove a slot assignment from a user participating in an active Guest Star session.
/// </summary>
/// <remarks>
/// This revokes their access to the session immediately and disables their access to publish or subscribe to media within the session.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageGuestStar"/> or <see cref="Scope.ModeratorManageGuestStar"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-guest-star-slot">Delete Guest Star Slot</see> for more information.
/// </remarks>
public record DeleteGuestStarSlotRequest
    : TwitchHelixRequest<DeleteGuestStarSlotResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/guest_star/slot";
    public override HttpMethod Method => HttpMethod.Delete;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageGuestStar, Scope.ModeratorManageGuestStar)
    };
    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("session_id", SessionId)
            .Add("guest_id", GuestId)
            .Add("slot_id", SlotId)
            .Add("should_reinvite_guest", ShouldReinviteGuest?.ToString());

    /// <summary>
    /// The user id of the broadcaster hosting the Guest Star session.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The id of the Guest Star session from which to remove a user.
    /// </summary>
    public required GuestStarSessionId SessionId { get; init; }

    /// <summary>
    /// The user id of the user to remove from the Guest Star session.
    /// </summary>
    public required UserId GuestId { get; init; }

    /// <summary>
    /// The id of the slot from which to remove the user from.
    /// </summary>
    public required GuestStarSlotId SlotId { get; init; }

    /// <summary>
    /// Determines whether the user should be reinvited to the session, sending them back to the invite queue.
    /// </summary>
    public bool? ShouldReinviteGuest { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<DeleteGuestStarSlotResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new DeleteGuestStarSlotResponseContent());
}
