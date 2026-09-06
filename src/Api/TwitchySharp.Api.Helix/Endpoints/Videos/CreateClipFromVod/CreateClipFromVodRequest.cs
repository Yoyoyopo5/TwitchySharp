using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Videos;
/// <summary>
/// Creates a clip from a broadcaster's VOD on behalf of the broadcaster or an editor of the channel.
/// </summary>
/// <remarks>
/// <para>
/// Since a live stream is actively creating a VOD, this endpoint can also be used to create a clip from earlier in the current stream.
/// The <see cref="CreatedVodClip.EditUrl"/> allows you to edit the clip's title, feature the clip, create a portrait version of the clip, download the clip media, and share the clip directly to social platforms.
/// </para>
/// <para>
/// Requires a user access token with <see cref="Scope.EditorManageClips"/> or <see cref="Scope.ChannelManageClips"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.EditorManageClips"/> or <see cref="Scope.ChannelManageClips"/> for the <see cref="EditorId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference#create-clip-from-vod">Create Clip From VOD</see> for more information.
/// </remarks>
public record CreateClipFromVodRequest
    : TwitchHelixRequest<CreateClipFromVodResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/videos/clips";
    public override HttpMethod Method => HttpMethod.Post;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(EditorId),
        ValidScopes = ImmutableHashSet.Create(Scope.EditorManageClips, Scope.ChannelManageClips)
    };
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("editor_id", EditorId)
            .Add("broadcaster_id", BroadcasterId)
            .Add("vod_id", VodId)
            .Add("vod_offset", ((int)VodOffset.TotalSeconds).ToString())
            .Add("duration", Duration?.TotalSeconds.ToString())
            .Add("title", Title);

    /// <summary>
    /// The user id of the editor of the channel to create a clip for.
    /// This should be the same user that created the user access token in the request,
    /// and it can be the broadcaster.
    /// </summary>
    public required UserId EditorId { get; init; }
    /// <summary>
    /// The user id of the broadcaster (channel) to create a clip for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The id of the VOD to create a clip for.
    /// </summary>
    public required VideoId VodId { get; init; }
    /// <summary>
    /// The end time of clip to create, measured from the start of the VOD.
    /// </summary>
    /// <remarks>
    /// The clip will start at <c><see cref="VodOffset"/> - <see cref="Duration"/></c>.
    /// If <see cref="Duration"/> is specified, this must be greater than its value.
    /// </remarks>
    public required TimeSpan VodOffset { get; init; }
    /// <summary>
    /// The duration of the clip to create.
    /// </summary>
    /// <remarks>
    /// Can range from 5 to 60 seconds, with a resolution of 100ms.
    /// If left <see langword="null"/>, defaults to 30 seconds.
    /// </remarks>
    public TimeSpan? Duration { get; init; }
    /// <summary>
    /// The title of the clip to create.
    /// </summary>
    public required string Title { get; init; }
}
