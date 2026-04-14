using Microsoft.CodeAnalysis;

namespace TwitchySharp.Helpers.Generators;

internal static class TypeKindExtensions
{
    public static string ToCodeString(this TypeKind typeKind)
        => typeKind switch
        {
            TypeKind.Struct or TypeKind.Structure => "struct",
            TypeKind.Class => "class",
            TypeKind.Interface => "interface",
            _ => string.Empty
        };
}
