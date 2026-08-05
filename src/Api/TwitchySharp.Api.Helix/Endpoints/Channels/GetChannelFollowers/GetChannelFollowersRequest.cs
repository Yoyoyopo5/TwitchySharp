using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets a list of users that follow the specified broadcaster.
/// You can also use this endpoint to see whether a specific user follows the broadcaster.
/// </summary>
/// <remarks>
/// For detailed follower information, the access token must be of a user that is either
/// 1) The broadcaster, or
/// 2) A moderator in the broadcaster's channel.
/// Otherwise, only the <see cref="GetChannelFollowersResponse.Total"/> will be provided.
/// <br/>
/// Requires a user access token with <see cref="Scope.ModeratorReadFollowers"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-followers">Get Channel Followers</see> for more information.
/// </remarks>
public record GetChannelFollowersRequest
    : TwitchHelixRequest<GetChannelFollowersResponse>, IForwardPageableRequest
{
    protected override string Path => "/channels/followers";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorReadFollowers)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("user_id", UserId)
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The user id of the broadcaster to get followers for.
    /// </summary>
    /// <remarks>
    /// Requires <see cref="Scope.ModeratorReadFollowers"/>.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// Use this parameter to see whether a specific user follows this broadcaster.
    /// </summary>
    /// <remarks>
    /// If specified, the response contains this user if they follow the broadcaster.
    /// If not specified, the response contains all users that follow the broadcaster.
    /// </remarks>
    public UserId? UserId { get; init; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// The minimum page size is 1 item per page and the maximum is 100.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <inheritdoc/>
    public PaginationCursor? After { get; init; }
}
