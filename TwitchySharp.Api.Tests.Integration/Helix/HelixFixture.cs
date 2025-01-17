using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Tests.Integration.Helix;
public class HelixFixture() : ApiFixture<HelixSecrets>("Helix");

[CollectionDefinition("helix")]
public class HelixCollection : ICollectionFixture<HelixFixture> { }

public record HelixSecrets
{
    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
    public required string UserAccessToken { get; init; }
    public required string AppAccessToken { get; init; }
}
