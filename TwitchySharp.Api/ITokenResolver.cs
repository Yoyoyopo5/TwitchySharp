using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api;

public interface ITokenResolver
{
    ValueTask<AccessToken> GetToken(TwitchApiIdentity identity, IEnumerable<Scope> validScopes, CancellationToken ct = default);
}



public class DefaultTokenResolver
    : ITokenResolver
{
    public ValueTask<AccessToken> GetToken(TwitchApiIdentity identity, IEnumerable<Scope> validScopes, CancellationToken ct = default)
    {
        
    }
}
