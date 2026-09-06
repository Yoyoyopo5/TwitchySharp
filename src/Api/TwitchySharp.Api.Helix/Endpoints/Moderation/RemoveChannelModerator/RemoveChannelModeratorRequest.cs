using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Removes a moderator from the broadcaster's chat room.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> A broadcaster may remove a maximum of 10 moderators within a 10-second window.
/// <para>
/// Requires a user access token that includes <see cref="Scope.ChannelManageModerators"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#remove-channel-moderator">Remove Channel Moderator</see> for more information.
/// </remarks>
public record RemoveChannelModeratorRequest
    : TwitchHelixRequest<RemoveChannelModeratorResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/moderation/moderators";
    public override HttpMethod Method => HttpMethod.Delete;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageModerators)
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
    /// The user id of the broadcaster (channel) to remove a moderator for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the moderator to remove from the broadcaster's channel.
    /// </summary>
    public required UserId UserId { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<RemoveChannelModeratorResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new RemoveChannelModeratorResponseContent());
}
