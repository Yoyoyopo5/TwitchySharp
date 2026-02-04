using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Defines methods for requesting new <see cref="UserAccessToken"/>s.
/// </summary>
public interface IRequestUserAccessToken
{
    /// <summary>
    /// Request a new <see cref="UserAccessTokenDetails"/> for the given <see cref="UserAccessTokenKey"/>.
    /// </summary>
    /// <remarks>
    /// Since obtaining a new <see cref="UserAccessToken"/> requires manual user interaction,
    /// this method returns a simple <see cref="ValueTask"/> whose completion indicate the request
    /// has been noted. Use a <see cref="IUserAccessTokenStore"/> to actually register the new token
    /// when it is available.
    /// </remarks>
    /// <param name="key">The key containing information about the <see cref="UserAccessToken"/> to create.</param>
    /// <returns>A <see cref="ValueTask"/>.</returns>
    ValueTask RequestUserAccessToken(UserAccessTokenKey key, CancellationToken ct = default);
}
