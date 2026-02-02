using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Default client identity resolver that first checks the request's configured identity,
/// then falls back to a provided default client identity.
/// </summary>
/// <param name="DefaultClientIdentity">The default <see cref="ClientIdentity"/> to use when a request doesn't specify one.</param>
/// <remarks>
/// Resolution order:
/// <list type="number">
/// <item>
/// Request's configured identity (from <see cref="IRequireAuthorization.Identity"/>)
/// </item>
/// <item>
/// Default client identity provided to constructor
/// </item>
/// </list>
/// <para>
/// This should cover most common scenarios where a single client created on the <see href="https://dev.twitch.tv/console">Twitch Developer Console</see> is used for an app.
/// If you have more complex needs (like multi-tenant apps), consider implementing <see cref="IResolveClientIdentity"/> directly and passing it to a <see cref="SequentialClientIdentityResolver"/> to define your own resolution pipeline.
/// </para>
/// </remarks>
public record DefaultClientIdentityResolver(ClientIdentity DefaultClientIdentity) : IResolveClientIdentity
{
    private readonly SequentialClientIdentityResolver _resolver = new(
    [
        new ConfiguredClientIdentityResolver(),
        new SingleClientIdentityResolver(DefaultClientIdentity)
    ]);

    /// <inheritdoc/>
    public ValueTask<ClientIdentity?> GetClientId(ITwitchRequest request, CancellationToken ct = default)
        => _resolver.GetClientId(request, ct);
}
