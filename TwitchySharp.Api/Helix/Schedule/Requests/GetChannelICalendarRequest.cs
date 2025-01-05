using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Gets the broadcaster’s streaming schedule as an <see href="https://datatracker.ietf.org/doc/html/rfc5545">iCalendar</see>.
/// </summary>
/// <param name="broadcasterId">The user id of the broadcaster (channel) to get the streaming schedule for.</param>
public class GetChannelICalendarRequest(string broadcasterId)
    : TwitchApiRequest<string>(
        "/helix/schedule/icalendar" + 
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
        )
{
    public override string BaseUrl => "https://api.twitch.tv";
    public override HttpMethod Method => HttpMethod.Get;
    public override string? Data => null;
    public override string? ContentType => null;
}
