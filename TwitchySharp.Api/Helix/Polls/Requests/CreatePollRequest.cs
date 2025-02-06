using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Helix.Polls;
/// <summary>
/// Creates a poll that viewers in the broadcaster’s channel can vote on.
/// The poll begins as soon as it’s created. A broadcaster may run only one poll at a time.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-poll">create poll</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelManagePolls"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManagePolls"/>.</param>
/// <param name="poll">The poll to create.</param>
public class CreatePollRequest(
    string clientId,
    string accessToken,
    CreatePollRequestData poll
    )
    : HelixApiRequest<CreatePollResponse, CreatePollRequestData>(
        "/polls",
        clientId,
        accessToken,
        poll
        );

/// <summary>
/// Used to create a new poll.
/// </summary>
public record CreatePollRequestData
{
    /// <summary>
    /// The user id of the broadcaster (channel) to create the poll for.
    /// This must be the same user that created the user access token in the request.
    /// </summary>
    public required string BroadcasterId { get; set; }
    /// <summary>
    /// The question that viewers will vote on.
    /// The question may contain a maximum of 60 characters.
    /// </summary>
    public required string Title { get; set; }
    /// <summary>
    /// A list of choices that viewers may choose from. 
    /// The list must contain a minimum of 2 choices and up to a maximum of 5 choices.
    /// </summary>
    public required CreatePollChoice[] Choices { get; set; }
    /// <summary>
    /// The length of time that the poll will run for. 
    /// The minimum is 15 seconds and the maximum is 1800 seconds (30 minutes).
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan Duration { get; set; }
    /// <summary>
    /// Determines whether viewers may cast additional votes using Channel Points.
    /// If set to <see langword="true"/>, the amount of Channel Points required per additional vote is set by <see cref="ChannelPointsPerVote"/>.
    /// The default value is <see langword="false"/>.
    /// </summary>
    public bool? ChannelPointsVotingEnabled { get; set; }
    /// <summary>
    /// If <see cref="ChannelPointsVotingEnabled"/> is set to <see langword="true"/>, the amount of points required to cast one additional vote.
    /// The minimum value is 1 and the maximum is 1,000,000.
    /// </summary>
    public int? ChannelPointsPerVote { get; set; }
}

/// <summary>
/// A choice for a new poll.
/// </summary>
public record CreatePollChoice
{
    /// <summary>
    /// The title of the choice that is visible to viewers.
    /// This may contain up to 25 characters.
    /// </summary>
    public required string Title { get; set; }
}
