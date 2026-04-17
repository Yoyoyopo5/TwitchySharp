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
public class ValueWrapperGenerator : IIncrementalGenerator
{
    private readonly static Lazy<Template> _template = new(() =>
    {
        if (typeof(ValueWrapperGenerator).Assembly.GetManifestResourceStream(ValueWrapperConstants.SCRIBAN_TEMPLATE_FILENAME) is not Stream templateStream)
            throw new FileNotFoundException($"Scriban template file for {nameof(ValueWrapperGenerator)} was not found.");
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
            if (w is null || !w.ShouldRender)
                return;
            ctx.CancellationToken.ThrowIfCancellationRequested();
            ctx.AddSource($"{w!.Name}_Wrapper.g.cs", ValueWrapperTemplate.RenderPartialValueWrapper(w));
        });
    }
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
                wrapper.JsonCreateExpression
            });
}