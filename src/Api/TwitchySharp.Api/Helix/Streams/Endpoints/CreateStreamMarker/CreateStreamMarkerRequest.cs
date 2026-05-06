using System.Collections.Immutable;
using System.Net.Http;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Adds a marker to a live stream.
/// </summary>
/// <remarks>
/// A marker is an arbitrary point in a live stream that the broadcaster or editor wants to mark, so they can return to that spot later to create video highlights (see Video Producer, Highlights in the Twitch UX).
/// <para>
/// Note that you may not add markers to a stream if:
/// <list type="bullet">
///     <item>
///     The stream is not live.
///     </item>
///     <item>
///     The stream has disabled video on demand (VODs).
///     </item>
///     <item>
///     The stream is a premiere (a live, first-viewing event that combines uploaded videos with live chat).
///     </item>
///     <item>
///     The stream is a rerun.
///     </item>
/// </list>
/// </para>
/// Requires a user access token that includes <see cref="Scope.ChannelManageBroadcast"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-stream-marker">Create Stream Marker</see> for more information.
/// </remarks>
public record CreateStreamMarkerRequest
    : TwitchHelixRequest<CreateStreamMarkerResponse>
{
    protected override string Path => "/streams/markers";
    public override HttpMethod Method => HttpMethod.Post;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(Marker.UserId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageBroadcast)
    };
    public override object? ContentObject => Marker;

    /// <summary>
    /// The marker to create.
    /// </summary>
    public required CreateStreamMarkerRequestData Marker { get; init; }
}

/// <summary>
/// Used to create a marker on a stream for future use.
/// </summary>
public record CreateStreamMarkerRequestData
{
    /// <summary>
    /// The user id of the broadcaster to create a marker for.
    /// This user or one of this broadcaster's editors must have created the user access token used in the <see cref="CreateStreamMarkerRequest"/>.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// A short description of the marker to help the user remember why they marked the location. 
    /// The maximum length of the description is 140 characters.
    /// </summary>
    public string? Description { get; init; }
}
