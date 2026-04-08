using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scriban;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace TwitchySharp.Helpers.Generators;

public static class ValueWrapperConstants
{
    public const string FULLY_QUALIFIED_WRAPPER_INTERFACE_NAME = "TwitchySharp.Helpers.IWrapValue`1";
    public const string WRAPPER_INTERFACE_NAME = "IWrapValue";
    public const string JSON_CONVERTER_ATTRIBUTE_NAME = "System.Text.Json.Serialization.JsonConverterAttribute";
    public const string SCRIBAN_TEMPLATE_FILENAME = "TwitchySharp.Helpers.Generators.IWrapValue.ValueWrapper.scriban";
}

[Generator]
public class ValueWrapperMethodGenerator : IIncrementalGenerator
{
# pragma warning disable RS2008 // We can work on this later if we want a shipped ruleset.
    private static readonly DiagnosticDescriptor PartialModifierRequiredWarning = new(
        id: "VWG001",
        title: "The type must be partial",
        messageFormat: "Make '{0}' partial to enable generation of ToString and implicit operator methods",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
        );

    private static readonly DiagnosticDescriptor JsonConstructionMethodMissingWarning = new(
        id: "VWG002",
        title: "A method of creating the type from the underlying value must be present",
        messageFormat: "To enable a generated JsonConverter, '{0}' must have a public constructor taking a single parameter of the wrapped type or a Value property with an initializer setter",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
        );
# pragma warning restore RS2008

    private readonly static Lazy<Template> _template = new(() =>
    {
        if (typeof(ValueWrapperMethodGenerator).Assembly.GetManifestResourceStream(ValueWrapperConstants.SCRIBAN_TEMPLATE_FILENAME) is not Stream templateStream)
            throw new FileNotFoundException($"Scriban template file for {nameof(ValueWrapperMethodGenerator)} was not found.");
        string templateString;
        using (StreamReader reader = new(templateStream))
        {
            templateString = reader.ReadToEnd();
        }
        return Template.Parse(templateString);
    });
    private static Template ValueWrapperTemplate => _template.Value;

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<INamedTypeSymbol?> wrapperInterfaceProvider = context.CompilationProvider
            .Select(static (compilation, ct) => compilation.GetTypeByMetadataName(ValueWrapperConstants.FULLY_QUALIFIED_WRAPPER_INTERFACE_NAME));

        IncrementalValueProvider<INamedTypeSymbol?> jsonConverterAttributeProvider = context.CompilationProvider
            .Select(static (compilation, ct) => compilation.GetTypeByMetadataName(ValueWrapperConstants.JSON_CONVERTER_ATTRIBUTE_NAME));

        IncrementalValuesProvider<ValueWrapperDefinition?> provider = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: GeneratorExtensions.IsLikelyWrapperDeclaration,
            transform: GeneratorExtensions.GetValueWrapperSymbolOrDefault
            )
            .Where(m => m is not null)
            .Select(static (ctx, ct) => new ValueWrapperGeneratorState()
            {
                WrapperTypeSymbol = ctx
            })
            .Combine(wrapperInterfaceProvider)
            .Select(static (ctx, ct) => ctx.Left with
            {
                InterfaceTypeSymbol = ctx.Right
            })
            .Combine(jsonConverterAttributeProvider)
            .Select(static (ctx, ct) => ctx.Left with
            {
                JsonConverterAttributeSymbol = ctx.Right
            })
            .SelectValueWrapper()
            .Select((x, _) =>
            {
                return x;
            });

        context.RegisterSourceOutput(provider, (ctx, w) =>
        {
            if (w is not ValueWrapperDefinition wrapper)
                return;

            bool addValueProperty = wrapper.ValueProperty is null && wrapper.WrappedValueParameterName != "Value";
            bool addImplicitOperator = wrapper.ImplicitConversionOperator is null;
            bool addToStringOverride = wrapper.ToStringOverride is null;
            bool addJsonConverter = wrapper.JsonConverterAttribute is null && wrapper.JsonConverterAttributeType is not null;
            ReadOnlySpan<bool> generateMembers = stackalloc bool[] { addValueProperty, addImplicitOperator, addToStringOverride, addJsonConverter };

            static bool anyTrue(ReadOnlySpan<bool> flags)
            {
                foreach (bool flag in flags)
                    if (flag) return true;
                return false;
            }
            if (!anyTrue(generateMembers))
                return;

            if (!wrapper.IsPartial)
            {
                if (wrapper.Location.IsInSource)
                    ctx.ReportDiagnostic(Diagnostic.Create(
                        PartialModifierRequiredWarning,
                        wrapper.Location,
                        wrapper.TypeName
                        ));
                return;
            }

            foreach (ClassDefiniton parent in wrapper.Parents)
            {
                if (!parent.IsPartial)
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(
                        PartialModifierRequiredWarning,
                        parent.Location,
                        parent.TypeName
                        ));
                    return;
                }
            }

            ValueWrapperDefinition.JsonConstructionMethod constructionMethod = wrapper.GetJsonConstructionMethod();

            if (addJsonConverter && constructionMethod == ValueWrapperDefinition.JsonConstructionMethod.None)
            {
                if (wrapper.Location.IsInSource)
                    ctx.ReportDiagnostic(Diagnostic.Create(
                        JsonConstructionMethodMissingWarning,
                        wrapper.Location,
                        wrapper.TypeName
                        ));
                addJsonConverter = false;
            }

            string generatedSource = ValueWrapperTemplate.Render(new
            {
                wrapper.Namespace,
                wrapper.TypeName,
                IsStruct = wrapper.TypeKind == TypeKind.Struct || wrapper.TypeKind == TypeKind.Structure,
                Parents = wrapper.Parents.Select(p => p.ExtendPartial()).Reverse().ToArray(),
                WrappedTypeName = wrapper.WrappedType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
                PartialDefinition = wrapper.ExtendPartial(),
                AddJsonConverter = addJsonConverter,
                AddValueProperty = wrapper.ValueProperty is null && wrapper.WrappedValueParameterName != "Value",
                AddImplicitOperator = wrapper.ImplicitConversionOperator is null,
                AddToStringOverride = wrapper.ToStringOverride is null,
                JsonConstructorMethod = constructionMethod switch
                {
                    ValueWrapperDefinition.JsonConstructionMethod.None => "none",
                    ValueWrapperDefinition.JsonConstructionMethod.Constructor => "constructor",
                    ValueWrapperDefinition.JsonConstructionMethod.Initializer => "initializer",
                    _ => "invalid"
                },
            });

            ctx.AddSource($"{wrapper.TypeName}_ConversionMethods.g.cs", generatedSource);
        });
    }
}

internal static class GeneratorExtensions
{
    public static bool IsLikelyWrapperDeclaration(this SyntaxNode node, CancellationToken ct)
        => node is TypeDeclarationSyntax { BaseList: not null } declaration
            && declaration.BaseList.Types.Any(static t => t.ToString().Contains(ValueWrapperConstants.WRAPPER_INTERFACE_NAME));

    public static INamedTypeSymbol? GetValueWrapperSymbolOrDefault(this GeneratorSyntaxContext context, CancellationToken ct)
    {
        if (context.Node is not TypeDeclarationSyntax typeDeclaration)
            return null;

        if (context.SemanticModel.GetDeclaredSymbol(typeDeclaration, ct) is not INamedTypeSymbol symbol)
            return null;

        return symbol;
    }

    public static IncrementalValuesProvider<ValueWrapperDefinition?> SelectValueWrapper(this IncrementalValuesProvider<ValueWrapperGeneratorState> provider)
        => provider.Select((ctx, ct) =>
        {
            if (ctx.WrapperTypeSymbol is null || ctx.InterfaceTypeSymbol is null)
                return null;

            return ctx.WrapperTypeSymbol.ToValueWrapperOrDefault(ctx.InterfaceTypeSymbol, ctx.JsonConverterAttributeSymbol, ct);
        });

    private static ValueWrapperDefinition? ToValueWrapperOrDefault(this INamedTypeSymbol wrapperDeclaration, INamedTypeSymbol interfaceDeclaration, INamedTypeSymbol? jsonConverterAttributeTypeSymbol, CancellationToken ct)
    {
        if (wrapperDeclaration.AllInterfaces.FirstOrDefault(i => SymbolEqualityComparer.Default.Equals(i.ConstructedFrom, interfaceDeclaration)) is not INamedTypeSymbol interfaceSymbol)
            return null;

        if (!interfaceSymbol.IsGenericType || interfaceSymbol.TypeArguments.Length == 0)
            return null;

        if (interfaceSymbol.TypeArguments.FirstOrDefault() is not ITypeSymbol wrappedValueType)
            return null;

        return new ValueWrapperDefinition(wrapperDeclaration, wrappedValueType, jsonConverterAttributeTypeSymbol);
    }

    public static IReadOnlyList<INamedTypeSymbol> GetContainingTypes(this INamedTypeSymbol symbol)
    {
        List<INamedTypeSymbol> containingTypes = [];
        INamedTypeSymbol? current = symbol.ContainingType;

        while (current is not null)
        {
            containingTypes.Add(current);
            current = current.ContainingType;
        }

        return containingTypes;
    }

    public static string ToCodeString(this Accessibility accessibility)
        => accessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
            Accessibility.Private => "private",
            Accessibility.Protected => "protected",
            Accessibility.ProtectedAndInternal => "private protected",
            Accessibility.ProtectedOrInternal => "protected internal",
            _ => string.Empty
        };

    public static string ToCodeString(this TypeKind typeKind)
        => typeKind switch
        {
            TypeKind.Struct or TypeKind.Structure => "struct",
            TypeKind.Class => "class",
            TypeKind.Interface => "interface",
            _ => string.Empty
        };
}