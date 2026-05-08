using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = TwitchIdentity.None.Instance
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster (channel) to get the streaming schedule for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    protected override async ValueTask<GetChannelICalendarResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
    {
        using StreamReader sr = new(contentStream);
        return new GetChannelICalendarResponse(await sr.ReadToEndAsync(ct));
    }
}
