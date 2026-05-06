using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E;

public readonly record struct Client
{
    public required ClientId Id { get; init; }
    public required ClientSecret Secret { get; init; }
}
