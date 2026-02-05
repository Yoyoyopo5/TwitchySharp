using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

/// <summary>
/// Defines methods for storing, retrieving, and removing <see cref="UserAccessTokenDetails"/>s.
/// </summary>
public interface IStoreUserAccessTokens
    : IStoreAccessTokens<UserAccessToken, UserAccessTokenKey, UserAccessTokenDetails>;
