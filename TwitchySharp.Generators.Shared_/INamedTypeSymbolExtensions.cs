using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace TwitchySharp.Generators.Shared;

public static class INamedTypeSymbolExtensions
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
