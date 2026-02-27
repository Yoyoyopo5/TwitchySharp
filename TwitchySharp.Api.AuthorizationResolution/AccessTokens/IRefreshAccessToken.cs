using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.AuthorizationResolution;

public interface IRefreshAccessToken<TDetails>
    where TDetails : IAccessTokenDetails
{
    ValueTask<AccessTokenRefreshResult> Refresh(AccessTokenDetailsResolutionResult.Expired<TDetails> token, CancellationToken ct = default);
}