using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Core;

public interface ITwitchClient
{
    ValueTask<ITwitchResponse> SendAsync(ITwitchRequest request, CancellationToken ct = default);
    ValueTask<ITwitchResponse<TResponseContent>> SendAsync<TResponseContent>(ITwitchRequest<TResponseContent> request, CancellationToken ct = default);
}
