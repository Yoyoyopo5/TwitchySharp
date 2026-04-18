using Microsoft.CodeAnalysis;
using System.Linq;
using TwitchySharp.Generators.Shared;

namespace TwitchySharp.Helpers.Analyzers.IWrapValue;

internal readonly record struct StaticCreateMethodInfo
{
    public static StaticCreateMethodInfo? FromWrapperSymbol(ValueWrapperSymbol wrapper)
    {
        if (wrapper.TypeSymbol
            .GetMembers("Create")
            .OfType<IMethodSymbol>()
            .Where(m => m.IsStatic)
            .Where(m => m.HasExactParametersOfType(wrapper.WrappedTypeSymbol))
            .FirstOrDefault(m => SymbolEqualityComparer.Default.Equals(m.ReturnType, wrapper.TypeSymbol)) is null)
            return null;
        return new();
    }
}

internal readonly record struct WrapperConstructorInfo
{
    public static WrapperConstructorInfo? FromWrapperSymbol(ValueWrapperSymbol wrapper)
    {
        if (wrapper.TypeSymbol.InstanceConstructors
            .Where(c => c.HasExactParametersOfType(wrapper.WrappedTypeSymbol))
            .Select(static c => (IMethodSymbol?)c)
            .FirstOrDefault() is not IMethodSymbol constructor)
            return null;
        return new();
    }
}

internal readonly record struct ValuePropertyInfo
{
    public static ValuePropertyInfo? FromWrapperSymbol(ValueWrapperSymbol wrapper)
    {
        if (wrapper.TypeSymbol.GetMembers("Value")
            .OfType<IPropertySymbol>()
            .FirstOrDefault() is not IPropertySymbol valueProperty)
            return null;
        return new()
        {
            Initializable = valueProperty.SetMethod switch
            {
                { DeclaredAccessibility: Accessibility.Public or Accessibility.Internal } => true,
                _ => false
            },
            IsOfWrappedType = SymbolEqualityComparer.Default.Equals(valueProperty.Type, wrapper.WrappedTypeSymbol)
        };
    }
    public required bool Initializable { get; init; }
    public required bool IsOfWrappedType { get; init; }
}
