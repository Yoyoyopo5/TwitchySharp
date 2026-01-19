using System.Net.Http;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Gets the broadcaster’s streaming schedule as an <see href="https://datatracker.ietf.org/doc/html/rfc5545">iCalendar</see>.
/// </summary>
/// <remarks>
/// Does not require any access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-icalendar">Get Channel iCalendar</see> for more information.
/// </remarks>
public record GetChannelICalendarRequest
    : TwitchHelixRequest<GetChannelICalendarResponse>
{
    /// <param name="broadcasterId">The user id of the broadcaster (channel) to get the streaming schedule for.</param>
    public GetChannelICalendarRequest(string broadcasterId)
        : base(
            "/schedule/icalendar",
            null!, // We set these to null because this is the only helix endpoint
            null!, // that does not require client id or authorization headers.
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
            )
    {
        Method = HttpMethod.Get;
    }
}
