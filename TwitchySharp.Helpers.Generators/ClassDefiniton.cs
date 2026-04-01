using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchySharp.Helpers.Generators;

internal record ClassDefiniton(INamedTypeSymbol TypeSymbol)
{
    public IReadOnlyList<TypeDeclarationSyntax> Syntax { get; } = [.. TypeSymbol
        .DeclaringSyntaxReferences
        .Select(r => r.GetSyntax())
        .OfType<TypeDeclarationSyntax>()];
    public string Namespace { get; } = TypeSymbol.ContainingNamespace.ToDisplayString();
    public bool IsPartial => Syntax.Any(s => s.Modifiers.Any(SyntaxKind.PartialKeyword));
    public bool IsReadOnly { get; } = TypeSymbol.IsReadOnly;
    public bool IsRecord { get; } = TypeSymbol.IsRecord;
    public bool IsAbstract { get; } = TypeSymbol.IsAbstract;
    public bool IsSealed { get; } = TypeSymbol.IsSealed;
    public bool IsStatic { get; } = TypeSymbol.IsStatic;
    public Location Location { get; } = TypeSymbol.Locations.FirstOrDefault() ?? Location.None;
    public Accessibility Accessibility { get; } = TypeSymbol.DeclaredAccessibility;
    public TypeKind TypeKind { get; } = TypeSymbol.TypeKind;
    public string TypeName { get; } = TypeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
    public IReadOnlyList<string> TypeParameterNames { get; } = [.. TypeSymbol.TypeParameters.Select(tp => tp.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat))];
    public IReadOnlyList<ClassDefiniton> Parents { get; } = [.. TypeSymbol.GetContainingTypes().Select(t => new ClassDefiniton(t)) ];

    /// <summary>
    /// Creates a type declaration string that can be used to extend the partial class in another file.
    /// </summary>
    /// <remarks>
    /// E.g. <c>public readonly partial record struct ExampleStruct</c>
    /// </remarks>
    public string ExtendPartial()
    {
        StringBuilder builder = new();
        builder.Append(Accessibility.ToCodeString()).Append(" ");
        if (IsReadOnly) builder.Append("readonly ");
        if (IsAbstract && !IsStatic) builder.Append("abstract ");
        if (IsSealed && !IsStatic && TypeKind != TypeKind.Struct) builder.Append("sealed ");
        if (IsStatic) builder.Append("static ");
        builder.Append("partial ");
        if (IsRecord) builder.Append("record ");
        builder.Append(TypeKind.ToCodeString());
        builder.Append(" ");
        builder.Append(TypeName);
        return builder.ToString();
    }
}
