using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Polls;
/// <summary>
/// Ends an active poll.
/// </summary>
/// <remarks>
/// You have the option to end it or end it and archive it.
/// <para>
/// Requires a user access token that includes <see cref="Scope.ChannelManagePolls"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#end-poll">End Poll</see> for more information.
/// </remarks>
public record EndPollRequest
    : TwitchHelixRequest<EndPollResponseContent>,
    IAuthenticatedTwitchRequest<UserWithScopesAuthenticationContext>
{
    protected override string Path => "/polls";
    public override HttpMethod Method => HttpMethod.Patch;
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
