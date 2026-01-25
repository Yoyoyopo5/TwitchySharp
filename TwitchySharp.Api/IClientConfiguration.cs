using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

public interface IClientConfiguration
{
    ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default);
}
