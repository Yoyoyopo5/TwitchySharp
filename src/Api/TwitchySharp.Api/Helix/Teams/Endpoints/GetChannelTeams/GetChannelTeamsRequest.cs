using System.Net.Http;

namespace TwitchySharp.Api.Helix.Teams;
/// <summary>
/// Gets the list of Twitch teams that the broadcaster is a member of.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-teams">Get Channel Teams</see> for more information.
/// </remarks>
public record GetChannelTeamsRequest
    : TwitchHelixRequest<GetChannelTeamsResponse>
{
    protected override string Path => "/teams/channel";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster to get teams for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
}
