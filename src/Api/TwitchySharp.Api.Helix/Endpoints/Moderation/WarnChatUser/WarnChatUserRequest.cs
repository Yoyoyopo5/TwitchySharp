using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Warns a user in the specified broadcaster's chat room, preventing them from chat interaction until the warning is acknowledged.
/// </summary>
/// <remarks>
/// New warnings can be issued to a user when they already have a warning in the channel (new warning will replace old warning).
/// <para>
/// Requires a user access token with <see cref="Scope.ModeratorManageWarnings"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorManageWarnings"/> for the <see cref="ModeratorId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#warn-chat-user">Warn Chat User</see> for more information.
/// </remarks>
public record WarnChatUserRequest
    : TwitchHelixRequest<WarnChatUserResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/moderation/warnings";
    public override HttpMethod Method => HttpMethod.Post;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageWarnings)
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
    public override object? ContentObject => Warning;

    /// <summary>
    /// The user id of the broadcaster (channel) to issue to warning in.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The warning data.
    /// </summary>
    public required WarnChatUserRequestData Warning { get; init; }
}

/// <summary>
/// Request data for a <see cref="WarnChatUserRequest"/>.
/// </summary>
public record WarnChatUserRequestData
{
    /// <summary>
    /// The warning data.
    /// </summary>
    public required ChatUserWarning Data { get; init; }
}

/// <summary>
/// Contains information about a specific chat warning.
/// </summary>
public record ChatUserWarning
{
    /// <summary>
    /// The id of the user to warn.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The custom reason for the warning.
    /// </summary>
    /// <remarks>
    /// Can be a maximum of 500 characters.
    /// </remarks>
    public required string Reason { get; init; }
}
