using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets a list of broadcasters that the specified user follows.
/// You can also use this endpoint to see whether a user follows a specific broadcaster.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.UserReadFollows"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-followed-channels">Get Followed Channels</see> for more information.
/// </remarks>
public record GetFollowedChannelsRequest
    : TwitchHelixRequest<GetFollowedChannelsResponse>, IForwardPageableRequest
{
    protected override string Path => "/channels/followed";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(UserId),
        ValidScopes = ImmutableHashSet.Create(Scope.UserReadFollows)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("user_id", UserId)
            .Add("broadcaster_id", BroadcasterId)
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The id of the user to get follows for.
    /// </summary>
    /// <remarks>
    /// This must be the user that created the access token used in the request.
    /// Requires <see cref="Scope.UserReadFollows"/>.
    /// </remarks>
    public required UserId UserId { get; init; }

    /// <summary>
    /// Use this parameter to see whether the user follows a specific broadcaster.
    /// </summary>
    /// <remarks>
    /// If specified, the response contains this broadcaster if the user follows them.
    /// If not specified, the response contains all broadcasters that the user follows.
    /// </remarks>
    public UserId? BroadcasterId { get; init; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100.
    /// The default is 20.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }
}
