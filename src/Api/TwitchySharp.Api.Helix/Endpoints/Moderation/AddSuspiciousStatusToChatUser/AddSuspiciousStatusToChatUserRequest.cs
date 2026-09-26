using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Adds a suspicious user status to a chatter on the broadcaster's channel.
/// </summary>
/// <remarks>
/// <para>
/// Requires a user access token with <see cref="Scope.ModeratorManageSuspiciousUsers"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorManageSuspiciousUsers"/> for the <see cref="ModeratorId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#add-suspicious-status-to-chat-user">Add Suspicious Status to Chat User</see> for more information.
/// </remarks>
public record AddSuspiciousStatusToChatUserRequest
    : TwitchHelixRequest<AddSuspiciousStatusToChatUserResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/moderation/suspicious_users";
    public override HttpMethod Method => HttpMethod.Post;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageSuspiciousUsers)
    };
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);

    public override object? ContentObject => Data;

    /// <summary>
    /// The user id of the broadcaster (channel) in whose chat the suspicious user status is being applied.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the moderator (or the broadcaster) to update the suspicious user status on behalf of.
    /// </summary>
    /// <remarks>
    /// This should be the same user that created the user access token for the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The request data.
    /// </summary>
    public required AddSuspiciousStatusToChatUserRequestData Data { get; init; }
}

/// <summary>
/// Contains data used with a <see cref="AddSuspiciousStatusToChatUserRequest"/>.
/// </summary>
public record AddSuspiciousStatusToChatUserRequestData
{
    /// <summary>
    /// The id of the user to add suspicious user status to.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The type of suspicious user status to add.
    /// </summary>
    public required SuspiciousUserStatus Status { get; init; }
}
