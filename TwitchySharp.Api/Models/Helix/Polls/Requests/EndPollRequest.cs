using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models.Helix.Polls.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.Polls.Requests;
/// <summary>
/// Ends an active poll.
/// </summary>
/// <remarks>
/// You have the option to end it or end it and archive it.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManagePolls"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#end-poll">End Poll</see> for more information.
/// </remarks>
public record EndPollRequest
    : TwitchHelixRequest<EndPollResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManagePolls"/>.</param>
    /// <param name="poll">Data used to end the poll.</param>
    public EndPollRequest(
        string clientId,
        string accessToken,
        EndPollRequestData poll
        ) : base(
            "/polls",
            clientId,
            accessToken
            )
    {
        Method = HttpMethod.Patch;
        ContentObject = poll;
    }
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
[JsonConverter(typeof(ValueBackedEnumJsonConverter<EndPollStatus, string>))]
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
