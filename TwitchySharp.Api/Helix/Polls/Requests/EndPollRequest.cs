using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Polls;
/// <summary>
/// Ends an active poll. 
/// You have the option to end it or end it and archive it.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#end-poll">end poll</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManagePolls"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManagePolls"/>.</param>
/// <param name="poll">Data used to end the poll.</param>
public class EndPollRequest(
    string clientId,
    string accessToken,
    EndPollRequestData poll
    )
    : HelixApiRequest<EndPollResponse, EndPollRequestData>(
        "/polls",
        clientId,
        accessToken,
        poll
        )
{
    public override HttpMethod Method => HttpMethod.Patch;
}

/// <summary>
/// Used to select a poll to end.
/// </summary>
public record EndPollRequestData
{
    /// <summary>
    /// The user id of the broadcaster (channel) that is running the poll to end.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The id of the poll to end.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The status to set the poll to.
    /// </summary>
    public required EndPollStatus Status { get; init; }
}

/// <summary>
/// Contains static references for valid poll end statuses.
/// </summary>
/// <param name="Value">The string value of the status to end the poll with.</param>
public record EndPollStatus(string Value)
    : ValueBackedEnum<string>(Value)
{
    /// <summary>
    /// Ends the poll before the poll is scheduled to end. 
    /// The poll remains publicly visible.
    /// </summary>
    public static EndPollStatus Terminated { get; } = new("TERMINATED");
    /// <summary>
    /// Ends the poll before the poll is scheduled to end, and then archives it so it's no longer publicly visible.
    /// </summary>
    public static EndPollStatus Archived { get; } = new("ARCHIVED");
}
