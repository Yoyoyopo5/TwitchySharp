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
        => (await Api.SendRequestAsync(new ValidateAccessTokenRequest(Secrets.User.AccessToken))).UserId;
}

[CollectionDefinition("helix")]
public class HelixCollection : ICollectionFixture<HelixFixture> { }

public record HelixSecrets
{
    public required ClientSecrets Client { get; init; }
    public required ExtensionSecrets Extension { get; init; }
    public required UserSecrets User { get; init; }
}

public record ClientSecrets
{
    public required string Id { get; init; }
    public required string Secret { get; init; }
}

public record ExtensionSecrets
{
    public required string Id { get; init; }
    public required string ApiSecret { get; init; }
    public required string Secret { get; init; }
    public required string Version { get; init; }
    public required ExtensionBitsProductSecrets BitsProduct { get; init; }
}

public record ExtensionBitsProductSecrets
{
    public required string Sku { get; init; }
}

public record UserSecrets
{
    public required string AccessToken { get; init; }
}
