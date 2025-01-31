using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Authorization.ClientUrls;

namespace TwitchySharp.Api.Tests.Integration.Helix;
public class HelixFixture() : ApiFixture<HelixSecrets>("Helix")
{
    public async Task<string> GetUserIdFromAccessTokenAsync()
        => (await Api.SendRequestAsync(new ValidateAccessTokenRequest(Secrets.UserAccessToken))).UserId;
}

[CollectionDefinition("helix")]
public class HelixCollection : ICollectionFixture<HelixFixture> { }

public record HelixSecrets
{
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
    public required string ExtensionClientId { get; init; }
    public required string ExtensionClientSecret { get; init; }
    public required string ExtensionSecret { get; init; }
    public required string UserAccessToken { get; init; }
}
