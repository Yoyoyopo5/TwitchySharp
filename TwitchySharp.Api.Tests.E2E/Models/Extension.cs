using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E;

public readonly record struct Extension
{
    public required ExtensionId Id { get; init; }
    public required ClientSecret Secret { get; init; }
    public required ExtensionSecret SharedSecret { get; init; }
    public required ExtensionVersion Version { get; init; }
    public required ExtensionBitsProduct BitsProduct { get; init; }
}

public readonly record struct ExtensionBitsProduct
{
    public required ExtensionProductSku Sku { get; init; }
}
