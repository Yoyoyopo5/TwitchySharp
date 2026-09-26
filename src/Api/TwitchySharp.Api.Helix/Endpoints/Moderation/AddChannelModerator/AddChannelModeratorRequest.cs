using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Adds a moderator to the broadcaster's chat room.
/// </summary>
/// <remarks>
/// <b>Rate Limits:</b> A broadcaster may add a maximum of 10 moderators within a 10-second window.
/// <para>
/// Requires a user access token that includes <see cref="Scope.ChannelManageModerators"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#add-channel-moderator">Add Channel Moderator</see> for more information.
/// </remarks>
public record AddChannelModeratorRequest
    : TwitchHelixRequest<AddChannelModeratorResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/moderation/moderators";
    public override HttpMethod Method => HttpMethod.Post;
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
    /// The user id of the broadcaster (channel) to add a moderator for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The id of the user to add as a moderator.
    /// </summary>
    public required UserId UserId { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<AddChannelModeratorResponseContent>>? ConvertResponseContent { get; init; }
        = (_, _) => ValueTask.FromResult(new AddChannelModeratorResponseContent());
}
