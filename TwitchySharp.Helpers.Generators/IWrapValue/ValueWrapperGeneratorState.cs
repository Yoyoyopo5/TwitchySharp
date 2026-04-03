using Microsoft.CodeAnalysis;
using System;

namespace TwitchySharp.Helpers.Generators;

internal record ValueWrapperGeneratorState
{
    public INamedTypeSymbol? WrapperTypeSymbol { get; init; }
    public INamedTypeSymbol? InterfaceTypeSymbol { get; init; }
    public INamedTypeSymbol? JsonConverterAttributeSymbol { get; init; }
}
