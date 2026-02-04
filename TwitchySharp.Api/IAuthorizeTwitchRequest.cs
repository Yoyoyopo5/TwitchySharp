using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Determines the authorization headers to use for a Twitch API request.
/// </summary>
/// <remarks>
/// Use <see cref="DefaultRequestAuthorizer"/> for standard authorization scenarios.
/// </remarks>
public interface IAuthorizeTwitchRequest
{
    /// <summary>
    /// Gets the authorization options for the given request.
    /// </summary>
    /// <param name="request">The full request that needs authorization.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>
    /// The authorization options containing the ClientId and BearerToken to set as headers,
    /// or <see langword="null"/> if the request does not require authorization.
    /// </returns>
    ValueTask<TwitchAuthorizationRequestOptions?> GetAuthorization(ITwitchRequest request, CancellationToken ct = default);
}