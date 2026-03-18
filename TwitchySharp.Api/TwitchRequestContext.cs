using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api;

public record TwitchRequestContext
{
    public required TwitchRequest Request { get; init; }
    public TwitchAuthorizationHeaders AuthorizationHeaders { get; init; }

    public static implicit operator TwitchRequestContext(TwitchRequest request)
      => new() { Request = request };
}

public delegate ValueTask<TwitchResponse> TwitchRequestHandler(TwitchRequestContext context, CancellationToken ct = default);
