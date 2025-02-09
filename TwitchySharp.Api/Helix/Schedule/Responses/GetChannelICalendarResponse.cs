using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.ApiResponseConverters;

namespace TwitchySharp.Api.Helix.Schedule.Responses;
[ApiConverter(typeof(ChannelICalendarResponseConverter))]
public record GetChannelICalendarResponse(string ICalendarString);

internal class ChannelICalendarResponseConverter : IConvertApiResponse
{
    public async ValueTask<TResponse> Convert<TResponse>(HttpResponseMessage httpResponse, CancellationToken ct = default)
        => (TResponse)(new GetChannelICalendarResponse(await httpResponse.Content.ReadAsStringAsync()) as object);
} 
