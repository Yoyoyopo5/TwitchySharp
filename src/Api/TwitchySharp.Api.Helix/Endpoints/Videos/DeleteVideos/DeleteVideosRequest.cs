using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Deletes one or more videos.
/// </summary>
/// <remarks>
/// You may delete past broadcasts, highlights, or uploads.
/// <para>
/// Requires a user access token that includes <see cref="Scope.ChannelManageVideos"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#delete-videos">Delete Videos</see> for more information.
/// </remarks>
public record DeleteVideosRequest
    : TwitchHelixRequest<DeleteVideosResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/videos";
    public override HttpMethod Method => HttpMethod.Delete;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(UserId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageVideos)
    };
    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    /// <summary>
    /// The user id of the broadcaster who owns the videos to delete.
    /// </summary>
    public required UserId UserId { get; init; }

    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("id", Ids.Select(x => x.Value));

    /// <summary>
    /// The ids of the videos to delete.
    /// </summary>
    /// <remarks>
    /// You can delete a maximum of 5 videos per request. Ignores invalid video IDs.
    /// If the user doesn't have permission to delete one of the videos in the list, none of the videos are deleted.
    /// </remarks>
    public required IEnumerable<VideoId> Ids { get; init; }
}
