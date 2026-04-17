using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Linq;

namespace TwitchySharp.Helpers.Analyzers.IWrapValue;

internal readonly record struct ValueWrapperSymbol
{
    public required INamedTypeSymbol TypeSymbol { get; init; }
    public required INamedTypeSymbol WrappedTypeSymbol { get; init; }
}

internal static class ValueWrapperSymbolExtensions
{
    extension(ValueWrapperSymbol wrapper)
    {
        public StaticCreateMethodInfo? GetWrapperStaticCreateMethodOrDefault()
            => StaticCreateMethodInfo.FromWrapperSymbol(wrapper);

        public WrapperConstructorInfo? GetWrapperConstructorOrDefault()
            => WrapperConstructorInfo.FromWrapperSymbol(wrapper);

        public ValuePropertyInfo? GetWrapperValuePropertyOrDefault()
            => ValuePropertyInfo.FromWrapperSymbol(wrapper);
    }
}

internal static partial class INamedTypeSymbolExtensions
{
    private static AttributeData? GetAttributesOfType(this INamedTypeSymbol symbol, INamedTypeSymbol attributeTypeSymbol)
        => symbol.GetAttributes().FirstOrDefault(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass?.OriginalDefinition, attributeTypeSymbol.OriginalDefinition));

    public static ValueWrapperSymbol? AsValueWrapperSymbol(this INamedTypeSymbol symbol, INamedTypeSymbol wrapperAttributeTypeSymbol)
        => symbol.GetAttributesOfType(wrapperAttributeTypeSymbol) switch
        {
            AttributeData wrapperAttributeData => wrapperAttributeData.AttributeClass?.TypeArguments.FirstOrDefault() switch
            {
                INamedTypeSymbol wrappedTypeSymbol => new ValueWrapperSymbol()
                {
                    TypeSymbol = symbol,
                    WrappedTypeSymbol = wrappedTypeSymbol,
                },
                _ => null
            },
            _ => null
        };
}
