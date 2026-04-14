using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitchySharp.Helpers.Generators;

internal static class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Use syntax nodes to determine if the type symbol is declared as partial.
    /// </summary>
    /// <param name="typeSymbol"></param>
    /// <returns></returns>
    public static bool IsPartial(this INamedTypeSymbol typeSymbol)
        => typeSymbol
            .DeclaringSyntaxReferences
            .Select(r => r.GetSyntax())
            .OfType<TypeDeclarationSyntax>()
            .Any(s => s.Modifiers.Any(SyntaxKind.PartialKeyword));

    /// <summary>
    /// Create the partial type declaration header for a type symbol.
    /// </summary>
    /// <param name="typeSymbol"></param>
    /// <returns>e.g. <c>"public partial record struct ExtendedType"</c></returns>
    public static string ToPartialTypeDeclarationString(this INamedTypeSymbol typeSymbol)
    {
        StringBuilder builder = new();
        builder.Append(typeSymbol.DeclaredAccessibility.ToCodeString()).Append(" ");
        if (typeSymbol.IsReadOnly) builder.Append("readonly ");
        if (typeSymbol.IsAbstract && !typeSymbol.IsStatic) builder.Append("abstract ");
        if (typeSymbol.IsSealed && !typeSymbol.IsStatic && typeSymbol.TypeKind != TypeKind.Struct) builder.Append("sealed ");
        if (typeSymbol.IsStatic) builder.Append("static ");
        builder.Append("partial ");
        if (typeSymbol.IsRecord) builder.Append("record ");
        builder.Append(typeSymbol.TypeKind.ToCodeString());
        builder.Append(" ");
        builder.Append(typeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat));
        return builder.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="symbol"></param>
    /// <returns>The type's containing types</returns>
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
}
