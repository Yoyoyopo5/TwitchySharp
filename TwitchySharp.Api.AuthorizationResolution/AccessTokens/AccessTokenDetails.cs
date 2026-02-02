using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;

public abstract record AccessTokenDetails
{
    public required DateTimeOffset ExpiresAt { get; init; }
}