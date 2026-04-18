using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Threading;

namespace TwitchySharp.Generators.Shared;

public static class IMethodSymbolExtensions
{
    public static bool IsPrimaryConstructor(this IMethodSymbol constructorSymbol, CancellationToken ct = default)
        => constructorSymbol.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax(ct) is TypeDeclarationSyntax;

    public static bool HasExactParametersOfType(this IMethodSymbol methodSymbol, params INamedTypeSymbol[] parameterTypes)
        => methodSymbol.Parameters.Length == parameterTypes.Length 
        && methodSymbol.Parameters.Select(p => p.Type).Zip(parameterTypes, SymbolEqualityComparer.Default.Equals).All(r => r == true);
}
