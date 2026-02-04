using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Resolves an <see cref="AppAccessToken"/> for a given <see cref="ClientIdentity"/>.
/// </summary>
public interface IResolveAppAccessToken : IResolveAccessToken<ClientIdentity>;
