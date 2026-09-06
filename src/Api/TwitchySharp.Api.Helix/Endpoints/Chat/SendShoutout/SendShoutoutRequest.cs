using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Sends a Shoutout to the specified broadcaster. See <see href="https://help.twitch.tv/s/article/shoutouts">Shoutouts</see>.
/// </summary>
/// <remarks>
/// A broadcaster may send a Shoutout once every 2 minutes. They may send the same broadcaster a Shoutout once every 60 minutes.
/// <para>
/// Requires a user access token with <see cref="Scope.ModeratorManageShoutouts"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorManageShoutouts"/> and <see cref="Scope.UserBot"/> for the <see cref="ModeratorId"/>,
/// and <see cref="Scope.ChannelBot"/> for the <see cref="FromBroadcasterId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#send-a-shoutout">Send a Shoutout</see> for more information.
/// </remarks>
public record SendShoutoutRequest
    : TwitchHelixRequest<SendShoutoutResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/chat/shoutouts";
    public override HttpMethod Method => HttpMethod.Post;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageShoutouts)
    };
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("from_broadcaster_id", FromBroadcasterId)
            .Add("to_broadcaster_id", ToBroadcasterId)
            .Add("moderator_id", ModeratorId);

    /// <summary>
    /// The user id of the broadcaster that's sending the shoutout.
    /// </summary>
    public required UserId FromBroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster that's receiving the shoutout.
    /// </summary>
    public required UserId ToBroadcasterId { get; init; }

    /// <summary>
    /// The user id of the moderator (or the broadcaster) to send the shoutout on behalf of.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// Requires <see cref="Scope.ModeratorManageShoutouts"/>.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<SendShoutoutResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new SendShoutoutResponseContent());
}
