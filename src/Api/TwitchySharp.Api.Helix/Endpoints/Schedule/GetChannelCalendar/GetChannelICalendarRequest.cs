namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Gets the broadcaster's streaming schedule as an <see href="https://datatracker.ietf.org/doc/html/rfc5545">iCalendar</see>.
/// </summary>
/// <remarks>
/// <para>
/// Does not require any access token.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-icalendar">Get Channel iCalendar</see> for more information.
/// </remarks>
public record GetChannelICalendarRequest
    : TwitchHelixRequest<GetChannelICalendarResponseContent>
{
    protected override string Path => "/schedule/icalendar";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster (channel) to get the streaming schedule for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    public override Func<Stream, CancellationToken, ValueTask<GetChannelICalendarResponseContent>>? ConvertResponseContent { get; init; }
        = async (contentStream, ct) =>
        {
            using StreamReader sr = new(contentStream);
            return new GetChannelICalendarResponseContent(await sr.ReadToEndAsync(ct));
        };
}
