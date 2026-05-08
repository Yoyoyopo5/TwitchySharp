using System.Collections.Immutable;
using System.Net.Http;

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
    protected override string Path => "/polls";
    public override HttpMethod Method => HttpMethod.Patch;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(Poll.BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManagePolls)
    };
    public override object? ContentObject => Poll;

    /// <summary>
    /// Data used to end the poll.
    /// </summary>
    public required EndPollRequestData Poll { get; init; }
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
