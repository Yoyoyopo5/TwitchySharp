using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System;
using System.Linq;
using System.Threading;
using TwitchySharp.Generators.Shared;

namespace TwitchySharp.Helpers.Analyzers.IWrapValue;

internal static class ValueWrapperConstants
{
    public const string WRAPPER_ATTRIBUTE_NAME = "TwitchySharp.Helpers.WrapperAttribute`1";
    public const string WRAPPER_INTERFACE_NAME = "global::TwitchySharp.Helpers.IWrapValue`2";
    public const string JSON_CONVERTER_ATTRIBUTE_TYPE_NAME = "System.Text.Json.Serialization.JsonConverterAttribute";
    public const string WRAPPER_JSON_CONVERTER_NAME = "global::TwitchySharp.Helpers.WrapperJsonConverter`2";
}

internal static class ValueWrapperDiagnostics
{
#pragma warning disable RS2008 // We can work on this later if we want a shipped ruleset.
    public static readonly DiagnosticDescriptor PartialModifierRequiredWarning = new(
        id: "VWG0001",
        title: "Wrapper type must be partial",
        messageFormat: "Make '{0}' partial to enable wrapper generation",
        category: "ValueWrapper",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
        );

    public static readonly DiagnosticDescriptor PartialParentTypesRequiredWarning = new(
        id: "VWG0002",
        title: "Wrapper containing types must be partial",
        messageFormat: "Make '{0}' partial to enable wrapper generation for '{1}'",
        category: "ValueWrapper",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
        );

    public static readonly DiagnosticDescriptor JsonConstructionMethodMissingWarning = new(
        id: "VWG0003",
        title: "Wrapper type is not constructable",
        messageFormat: "To enable wrapper Json deserialization, '{0}' must have: " +
        "(1) a public constructor taking a single argument of the wrapped type, " +
        "(2) a publicly initializable Value property of the wrapped type, or " +
        "(3) a static Create method taking a single argument of the wrapped type and returning an instance of {0}",
        category: "ValueWrapper",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
        );

    public static readonly DiagnosticDescriptor WronglyTypedValueProperty = new(
        id: "VWG0004",
        title: "Value property is not wrapped type",
        messageFormat: "'{0}' must have a Value property or record primary constructor of type '{1}' to enable wrapper generation",
        category: "ValueWrapper",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true
        );

# pragma warning restore RS2008
}

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class ValueWrapperAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(
        ValueWrapperDiagnostics.PartialModifierRequiredWarning,
        ValueWrapperDiagnostics.PartialParentTypesRequiredWarning,
        ValueWrapperDiagnostics.JsonConstructionMethodMissingWarning
    );

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSymbolAction(
            action: (context) =>
            {
                CancellationToken ct = context.CancellationToken;

                if (context.Compilation.GetTypeByMetadataName(ValueWrapperConstants.WRAPPER_ATTRIBUTE_NAME)
                    is not INamedTypeSymbol wrapperAttributeSymbol)
                    return;

                ct.ThrowIfCancellationRequested();
                if (((INamedTypeSymbol)context.Symbol).AsValueWrapperSymbol(wrapperAttributeSymbol) is not ValueWrapperSymbol wrapper)
                    return;

                wrapper
                    .AnalyzePartial(context.ReportDiagnostic, ct)
                    .AnalyzePartialParents(context.ReportDiagnostic, ct)
                    .AnalyzeJsonConstruction(context.ReportDiagnostic, ct);
            },
            symbolKinds: ImmutableArray.Create(SymbolKind.NamedType)
            );
    }
}

internal static class ValueWrapperSymbolAnalyzerExtensions
{
    public static ValueWrapperSymbol AnalyzePartial(this ValueWrapperSymbol wrapperSymbol, Action<Diagnostic> report, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        if (!wrapperSymbol.TypeSymbol.IsPartial())
            report(Diagnostic.Create(
                ValueWrapperDiagnostics.PartialModifierRequiredWarning,
                wrapperSymbol.TypeSymbol.Locations.FirstOrDefault(),
                wrapperSymbol.TypeSymbol.Name
                ));
        return wrapperSymbol;
    }

    public static ValueWrapperSymbol AnalyzePartialParents(this ValueWrapperSymbol wrapperSymbol, Action<Diagnostic> report, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        if (wrapperSymbol.TypeSymbol.GetContainingTypes().FirstOrDefault(parent => !parent.IsPartial()) is INamedTypeSymbol nonPartialParent)
            report(Diagnostic.Create(
                ValueWrapperDiagnostics.PartialParentTypesRequiredWarning,
                nonPartialParent.Locations.FirstOrDefault(),
                nonPartialParent.Name,
                wrapperSymbol.TypeSymbol.Name
                ));
        return wrapperSymbol;
    }

    public static ValueWrapperSymbol AnalyzeJsonConstruction(this ValueWrapperSymbol wrapperSymbol, Action<Diagnostic> report, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        if (wrapperSymbol.GetWrapperValuePropertyOrDefault() is not ValuePropertyInfo valueProperty)
            return wrapperSymbol; // Source generator will add the value property.
        if (wrapperSymbol.GetWrapperConstructorOrDefault() is null
            && !valueProperty.Initializable
            && wrapperSymbol.GetWrapperStaticCreateMethodOrDefault() is null)
            report(Diagnostic.Create(
                ValueWrapperDiagnostics.JsonConstructionMethodMissingWarning,
                wrapperSymbol.TypeSymbol.Locations.FirstOrDefault(),
                wrapperSymbol.TypeSymbol.Name
                ));
        return wrapperSymbol;
    }
}
