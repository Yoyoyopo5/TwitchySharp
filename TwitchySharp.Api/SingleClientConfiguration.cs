using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

public record SingleClientConfiguration(ClientIdentity? ClientId) : IClientConfiguration
{
    ValueTask<ClientIdentity?> IClientConfiguration.GetClientId(ITwitchRequest request, CancellationToken ct)
        => ValueTask.FromResult(ClientId);
}
