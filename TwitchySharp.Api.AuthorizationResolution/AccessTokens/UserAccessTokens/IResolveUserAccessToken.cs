using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;
/// <summary>
/// Resolves a <see cref="UserAccessToken"/> for a given <see cref="UserAccessTokenKey"/>.
/// </summary>
public interface IResolveUserAccessToken : IResolveAccessToken<UserAccessTokenKey>;
