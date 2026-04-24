using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

internal record TwitchClient : ITwitchClient
{
    public required TwitchRequestHandler RequestHandler { get; init; }

    public ValueTask<TwitchResponse> SendAsync(TwitchRequest request, CancellationToken ct = default)
        => RequestHandler(request, ct);

    public async ValueTask<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(TwitchRequest<TResponseContent> request, CancellationToken ct = default)
        => (TwitchResponse<TResponseContent>)await RequestHandler(request, ct).ConfigureAwait(false);
}
