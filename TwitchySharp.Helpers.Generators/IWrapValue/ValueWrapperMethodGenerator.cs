using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scriban;
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace TwitchySharp.Helpers.Generators;

public static class ValueWrapperConstants
{
    public const string WRAPPER_ATTRIBUTE_NAME = "TwitchySharp.Helpers.WrapperAttribute`1";
    public const string WRAPPER_INTERFACE_NAME = "global::TwitchySharp.Helpers.IWrapValue`2";
    public const string JSON_CONVERTER_ATTRIBUTE_TYPE_NAME = "System.Text.Json.Serialization.JsonConverterAttribute";
    public const string WRAPPER_JSON_CONVERTER_NAME = "global::TwitchySharp.Helpers.WrapperJsonConverter`2";
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
        IncrementalValuesProvider<ValueWrapperDefinition?> provider = context.SyntaxProvider.ForAttributeWithMetadataName(
            ValueWrapperConstants.WRAPPER_ATTRIBUTE_NAME,
            predicate: static (node, _) => node is TypeDeclarationSyntax,
            transform: ValueWrapperDefinition.FromAttributeContext
            );

        context.RegisterSourceOutput(provider, (ctx, w) =>
        {
            if (!w.ShouldRender()) 
                return;
            ctx.CancellationToken.ThrowIfCancellationRequested();
            ctx.AddSource($"{w!.Name}_Wrapper.g.cs", ValueWrapperTemplate.RenderPartialValueWrapper(w));

            //if (!wrapper.IsPartial)
            //{
            //    if (wrapper.Location.IsInSource)
            //        ctx.ReportDiagnostic(Diagnostic.Create(
            //            PartialModifierRequiredWarning,
            //            wrapper.Location,
            //            wrapper.TypeName
            //            ));
            //    return;
            //}

            //foreach (ClassDefiniton parent in wrapper.Parents)
            //{
            //    if (!parent.IsPartial)
            //    {
            //        ctx.ReportDiagnostic(Diagnostic.Create(
            //            PartialModifierRequiredWarning,
            //            parent.Location,
            //            parent.TypeName
            //            ));
            //        return;
            //    }
            //}

            //if (addJsonConverter && constructionMethod == ValueWrapperDefinition.JsonConstructionMethod.None)
            //{
            //    if (wrapper.Location.IsInSource)
            //        ctx.ReportDiagnostic(Diagnostic.Create(
            //            JsonConstructionMethodMissingWarning,
            //            wrapper.Location,
            //            wrapper.TypeName
            //            ));
            //    addJsonConverter = false;
            //}
        });
    }
}

internal static class ValueWrapperDefinitionExtensions
{
    public static bool ShouldRender(this ValueWrapperDefinition? wrapper)
        => wrapper is not null
        && wrapper.IsPartial
        && wrapper.ParentTypes.All(p => p.IsPartial);
}

internal static class TemplateExtensions
{
    public static string RenderPartialValueWrapper(
        this Template wrapperTemplate,
        ValueWrapperDefinition wrapper)
        => wrapperTemplate.Render(
            new
            {
                wrapper.Namespace,
                TypeName = wrapper.Name,
                Parents = wrapper.ParentTypes.Reverse().Select(pt => pt.PartialDeclaration).ToArray(),
                WrappedTypeName = wrapper.WrappedType.FullyQualifiedName,
                PartialDefinition = wrapper.PartialDeclaration,
                JsonConverterType = wrapper.HasJsonConverterAttribute ? null : ValueWrapperConstants.WRAPPER_JSON_CONVERTER_NAME.Replace("`2", $"<{wrapper.Name}, {wrapper.WrappedType.FullyQualifiedName}>"),
                wrapper.ShouldAddValueProperty,
                ShouldAddImplicitOperator = wrapper.ImplicitOperator is null,
                ShouldAddToStringOverride = wrapper.ToStringOverride is null,
                WrapperInterface = ValueWrapperConstants.WRAPPER_INTERFACE_NAME.Replace("`2", $"<{wrapper.WrappedType.FullyQualifiedName}, {wrapper.Name}>"),
                JsonCreateExpression = wrapper switch // We actually supply the expression to create from value.
                {
                    { StaticCreateMethod: not null } => "Create(value)",
                    { WrapperConstructor: not null } => "new(value);",
                    { WrapperValueProperty: not null and { Initializable: true } } or { ShouldAddValueProperty: true } => $$"""new() { {{(wrapper.WrapperValueProperty.HasValue ? wrapper.WrapperValueProperty.Value.Name : "Value")}} = value };""",
                    _ => null
                }
            });
}