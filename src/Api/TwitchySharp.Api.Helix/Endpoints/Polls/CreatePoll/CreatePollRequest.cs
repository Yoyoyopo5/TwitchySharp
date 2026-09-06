using System.Collections.Immutable;
using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Polls;
/// <summary>
/// Creates a poll that viewers in the broadcaster's channel can vote on.
/// </summary>
/// <remarks>
/// The poll begins as soon as it's created. A broadcaster may run only one poll at a time.
/// <para>
/// Requires a user access token that includes <see cref="Scope.ChannelManagePolls"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-poll">Create Poll</see> for more information.
/// </remarks>
public record CreatePollRequest
    : TwitchHelixRequest<CreatePollResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/polls";
    public override HttpMethod Method => HttpMethod.Post;
    private UserWithScopesAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(Poll.BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManagePolls)
    };
    public UserWithScopesAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    public override object? ContentObject => Poll;

    /// <summary>
    /// The poll to create.
    /// </summary>
    public required CreatePollRequestData Poll { get; init; }
}

/// <summary>
/// Used to create a new poll.
/// </summary>
public record CreatePollRequestData
{
    /// <summary>
    /// The user id of the broadcaster (channel) to create the poll for.
    /// This must be the same user that created the user access token in the request.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The question that viewers will vote on.
    /// The question may contain a maximum of 60 characters.
    /// </summary>
    public required string Title { get; init; }
    /// <summary>
    /// A list of choices that viewers may choose from. 
    /// The list must contain a minimum of 2 choices and up to a maximum of 5 choices.
    /// </summary>
    public required CreatePollChoice[] Choices { get; init; }
    /// <summary>
    /// The length of time that the poll will run for. 
    /// The minimum is 15 seconds and the maximum is 1800 seconds (30 minutes).
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public required TimeSpan Duration { get; init; }
    /// <summary>
    /// Determines whether viewers may cast additional votes using Channel Points.
    /// If set to <see langword="true"/>, the amount of Channel Points required per additional vote is set by <see cref="ChannelPointsPerVote"/>.
    /// The default value is <see langword="false"/>.
    /// </summary>
    public bool? ChannelPointsVotingEnabled { get; init; }
    /// <summary>
    /// If <see cref="ChannelPointsVotingEnabled"/> is set to <see langword="true"/>, the amount of points required to cast one additional vote.
    /// The minimum value is 1 and the maximum is 1,000,000.
    /// </summary>
    public int? ChannelPointsPerVote { get; init; }
}
