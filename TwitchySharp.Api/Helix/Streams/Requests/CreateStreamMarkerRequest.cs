using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Streams.Requests;
/// <summary>
/// Adds a marker to a live stream. 
/// A marker is an arbitrary point in a live stream that the broadcaster or editor wants to mark, so they can return to that spot later to create video highlights (see Video Producer, Highlights in the Twitch UX).
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-stream-marker">create stream marker</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManageBroadcast"/>.
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
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageBroadcast"/>.</param>
/// <param name="marker">The marker to create.</param>
public class CreateStreamMarkerRequest(
    string clientId,
    string accessToken,
    CreateStreamMarkerRequestData marker
    )
    : HelixApiRequest<CreateStreamMarkerResponse, CreateStreamMarkerRequestData>(
        "/streams/markers",
        clientId,
        accessToken,
        marker
        );

/// <summary>
/// Used to create a marker on a stream for future use.
/// </summary>
public record CreateStreamMarkerRequestData
{
    /// <summary>
    /// The user id of the broadcaster to create a marker for.
    /// This user or one of this broadcaster's editors must have created the user access token used in the <see cref="CreateStreamMarkerRequest"/>.
    /// </summary>
    public required string UserId { get; set; }
    /// <summary>
    /// A short description of the marker to help the user remember why they marked the location. 
    /// The maximum length of the description is 140 characters.
    /// </summary>
    public string? Description { get; set; }
}
