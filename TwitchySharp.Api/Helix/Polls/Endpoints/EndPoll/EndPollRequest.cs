using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Polls;
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
        ClientId clientId,
        UserAccessToken accessToken,
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
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The id of the poll to end.
    /// </summary>
    public required PollId Id { get; init; }
    /// <summary>
    /// The status to set the poll to.
    /// </summary>
    public required EndPollStatus Status { get; init; }
}