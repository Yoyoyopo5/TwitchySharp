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
    public required ClientSecrets Client { get; init; }
    public required UserSecrets User { get; init; }
    public required DeviceSecrets Device { get; init; }
}

public record ClientSecrets
{
    public required string Id { get; init; }
    public required string Secret { get; init; }
    public required string RedirectUri { get; init; }
}

public record UserSecrets
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
    public required string AuthorizationCode { get; init; }
}

public record DeviceSecrets
{
    public required string AuthorizationCode { get; init; }
}
