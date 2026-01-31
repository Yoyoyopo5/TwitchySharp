using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Gets the broadcaster's streaming schedule as an <see href="https://datatracker.ietf.org/doc/html/rfc5545">iCalendar</see>.
/// </summary>
/// <remarks>
/// Does not require any access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-icalendar">Get Channel iCalendar</see> for more information.
/// </remarks>
public record GetChannelICalendarRequest
    : TwitchHelixRequest<GetChannelICalendarResponse>
{
    protected override string Path => "/schedule/icalendar";
    public override HttpMethod Method => HttpMethod.Get;
    // This endpoint does not require any authentication
    protected override TwitchApiIdentity DefaultIdentity => TwitchApiIdentity.None;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster (channel) to get the streaming schedule for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
}
