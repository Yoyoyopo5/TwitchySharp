using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Tests.Integration.Authorization;
public class AuthorizationFixture() : ApiFixture<AuthorizationSecrets>("Authorization");

[CollectionDefinition("authorization")]
public class AuthorizationCollection : ICollectionFixture<AuthorizationFixture> { }

public record AuthorizationSecrets
{
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
    public required string RedirectUri { get; init; }
    public required string UserAccessToken { get; init; }
    public required string UserRefreshToken { get; init; }
    public required string UserAuthorizationCode { get; init; }
    public required string DeviceAuthorizationCode { get; init; }
}
