using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.Schedule.Responses;

[ApiConverter(typeof(ChannelICalendarResponseConverter))]
public record GetChannelICalendarResponse(string ICalendarString);

internal class ChannelICalendarResponseConverter : IConvertResponseContent
{
    public async ValueTask<TResponse> Convert<TResponse>(HttpResponseMessage httpResponse, CancellationToken ct = default)
        => (TResponse)(new GetChannelICalendarResponse(await httpResponse.Content.ReadAsStringAsync(ct)) as object);
}
