using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

public record TwitchRequestContext
{
    public required TwitchRequest Request { get; init; }
    public TwitchAuthorizationHeaders AuthorizationHeaders { get; init; }

    public static implicit operator TwitchRequestContext(TwitchRequest request)
      => new() { Request = request };
}

public delegate ValueTask<TwitchResponse> TwitchRequestHandler(TwitchRequestContext context, CancellationToken ct = default);
