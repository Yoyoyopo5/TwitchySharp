using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TwitchySharp.Helpers.Generators;

internal record ValueWrapperDefinition(INamedTypeSymbol TypeSymbol, ITypeSymbol WrappedType, INamedTypeSymbol? JsonConverterAttributeType) : ClassDefiniton(TypeSymbol)
{
    public enum JsonConstructionMethod
    {
        None,
        /// <summary>
        /// Can be constructed via single Value parameter constructor.
        /// </summary>
        /// <remarks>
        /// <code>
        /// new Wrapper(value);
        /// </code>
        /// </remarks>
        Constructor,
        /// <summary>
        /// Can be constructed via Value property initializer.
        /// </summary>
        /// <remarks>
        /// <code>
        /// new Wrapper { Value = value };
        /// </code>
        /// </remarks>
        Initializer
    }
    public string? WrappedValueParameterName => Syntax.FirstOrDefault().ParameterList switch
    {
        { } parameterList when parameterList.Parameters.Count > 0
            => TypeSymbol.InstanceConstructors
                .FirstOrDefault(c => c.Locations.Any(l => l.SourceSpan.Contains(parameterList.Span))) switch
            {
                IMethodSymbol primaryConstructor => primaryConstructor.Parameters.FirstOrDefault(p => SymbolEqualityComparer.Default.Equals(p.Type, WrappedType))?.Name,
                _ => null
            },
        _ => null
    };
    public IPropertySymbol? ValueProperty { get; } = TypeSymbol.GetMembers("Value")
        .OfType<IPropertySymbol>()
        .FirstOrDefault(p => SymbolEqualityComparer.Default.Equals(p.Type, WrappedType));
    public IMethodSymbol? ImplicitConversionOperator { get; } = TypeSymbol.GetMembers()
        .OfType<IMethodSymbol>()
        .FirstOrDefault(m => m.MethodKind == MethodKind.Conversion
            && m.Name == "op_Implicit"
            && SymbolEqualityComparer.Default.Equals(m.ReturnType, WrappedType));
    public IMethodSymbol? ToStringOverride { get; } = TypeSymbol.GetMembers("ToString")
        .OfType<IMethodSymbol>()
        .FirstOrDefault(m => m.Parameters.Length == 0 && m.IsOverride);
    public AttributeData? JsonConverterAttribute { get; } = TypeSymbol.GetAttributes()
        .FirstOrDefault(attr => SymbolEqualityComparer.Default.Equals(attr.AttributeClass, JsonConverterAttributeType));
}

internal static class ValueWrapperDefinitionExtensions
{
    public static ValueWrapperDefinition.JsonConstructionMethod GetJsonConstructionMethod(this ValueWrapperDefinition wrapper)
    {
        // Check for a constructor with a single parameter of the wrapped type.
        if (wrapper.TypeSymbol.InstanceConstructors
            .Where(c => c.Parameters.Length == 1)
            .Where(c => c.DeclaredAccessibility is Accessibility.Public or Accessibility.Internal)
            .Any(c => SymbolEqualityComparer.Default.Equals(c.Parameters.First().Type, wrapper.WrappedType)))
            return ValueWrapperDefinition.JsonConstructionMethod.Constructor;

        IEnumerable<IPropertySymbol> properties = wrapper.TypeSymbol.GetMembers().OfType<IPropertySymbol>();

        IEnumerable<IPropertySymbol> requiredPropertiesOtherThanValue = properties.Where(p => p.IsRequired).Where(p => !(SymbolEqualityComparer.Default.Equals(p.Type, wrapper.WrappedType) && p.Name == "Value"));
        if (requiredPropertiesOtherThanValue.Any())
            return ValueWrapperDefinition.JsonConstructionMethod.None;

        // i.e., not generated, if this is null, a settable Value property will be generated.
        IPropertySymbol? preexistingValueProperty = properties.FirstOrDefault(p => SymbolEqualityComparer.Default.Equals(p.Type, wrapper.WrappedType) && p.Name == "Value");
        if (preexistingValueProperty is null)
            return ValueWrapperDefinition.JsonConstructionMethod.Initializer;

        if (preexistingValueProperty.SetMethod is null || preexistingValueProperty.SetMethod.DeclaredAccessibility is not Accessibility.Public and not Accessibility.Internal)
            return ValueWrapperDefinition.JsonConstructionMethod.None;

        return ValueWrapperDefinition.JsonConstructionMethod.Initializer;
    }
}