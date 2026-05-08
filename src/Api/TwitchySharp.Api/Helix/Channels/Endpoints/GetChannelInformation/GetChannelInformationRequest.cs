using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Gets information about one or more channels.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-information">Get Channel Information</see> for more information.
/// </remarks>
public record GetChannelInformationRequest
    : TwitchHelixRequest<GetChannelInformationResponse>
{
    protected override string Path => "/channels";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterIds.Select(x => x.ToString()));

    /// <summary>
    /// The user id of the broadcaster(s) whose channel information you want to get.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 100 ids. The API ignores duplicate IDs and IDs that are not found.
    /// </remarks>
    public required IEnumerable<UserId> BroadcasterIds { get; init; }
}
