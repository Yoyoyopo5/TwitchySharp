using System.Collections.Immutable;
using System.Net.Http;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// <b>BETA</b> Remove a suspicious user status from a chatter on broadcaster's channel.
/// </summary>
/// <remarks>
/// Requires an app or user access token that includes <see cref="Scope.ModeratorManageSuspiciousUsers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-suspicious-status-from-chat-user">Remove Suspicious Status from Chat User</see> for more information.
/// </remarks>
public record RemoveSuspiciousStatusFromChatUserRequest
    : TwitchHelixRequest<RemoveSuspiciousStatusFromChatUserResponse>
{
    protected override string Path => "/moderation/suspicious_users";
    public override HttpMethod Method => HttpMethod.Delete;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageSuspiciousUsers)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId)
            .Add("user_id", UserId);

    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat to remove the suspicious user status.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the moderator (or the broadcaster) to remove the suspicious user status on behalf of.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The id of the user to remove the suspicious user status on.
    /// </summary>
    public required UserId UserId { get; init; }
}
